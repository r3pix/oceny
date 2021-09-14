using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using oceny5._0.Entities;
using oceny5._0.Exceptions;
using oceny5._0.Models;

namespace oceny5._0.Services
{
    public interface IAdminService
    {
        Task<int> Create(CreateAdminDto dto);
        Task<string> GenerateJwtToken(LoginAdminDto dto);
    }

    public class AdminService : IAdminService
    {
        private readonly OcenyDBContext _dbContext;
        private readonly IPasswordHasher<Admin> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AdminService(OcenyDBContext dbContext,IPasswordHasher<Admin> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<int> Create(CreateAdminDto dto)
        {
            var admin = new Admin()
            {
                Email = dto.Email
            };

            admin.HashedPassword = _passwordHasher.HashPassword(admin,dto.Password);

            _dbContext.Admins.Add(admin);
            await _dbContext.SaveChangesAsync();

            return admin.Id;
        }

        public async Task<string> GenerateJwtToken(LoginAdminDto dto)
        {
            var admin = await _dbContext.Admins.FirstOrDefaultAsync(x=>x.Email == dto.Email);

            if (admin is null)
            {
                throw new BadRequestException("Bad credentials");
            }

            var result = _passwordHasher.VerifyHashedPassword(admin, admin.HashedPassword, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Bad credentials");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,admin.Id.ToString()),
                new Claim(ClaimTypes.Role,admin.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);

        }

    }
}
