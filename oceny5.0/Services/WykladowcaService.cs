using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using oceny5._0.Entities;
using oceny5._0.Exceptions;
using oceny5._0.Models;

namespace oceny5._0.Services
{
    public interface IWykladowcaService
    {
        Task<int> Create(CreateWykladowcaDto dto);
        Task<string> GenerateJwt(LoginWykladowcaDto dto);
    }

    public class WykladowcaService : IWykladowcaService
    {
        private readonly OcenyDBContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Wykladowca> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public WykladowcaService(OcenyDBContext context, IMapper mapper, IPasswordHasher<Wykladowca> passwordHasher,AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<int> Create(CreateWykladowcaDto dto)
        {
            var wykladowca = _mapper.Map<Wykladowca>(dto);
            wykladowca.HashedPassword = _passwordHasher.HashPassword(wykladowca, dto.Password);

            _context.Wykladowcy.Add(wykladowca);
            await _context.SaveChangesAsync();
            return wykladowca.Id;

        }

        public async Task<string> GenerateJwt(LoginWykladowcaDto dto)
        {
            var wykladowca = await _context.Wykladowcy.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (wykladowca is null)
            {
                throw new NotFoundException("The user does not exist");
            }

            var result = _passwordHasher.VerifyHashedPassword(wykladowca, wykladowca.HashedPassword, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Provided password is wrong");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,wykladowca.Id.ToString()),
                new Claim(ClaimTypes.Name,$"{wykladowca.Imie} {wykladowca.Nazwisko}"),
                new Claim(ClaimTypes.Role,wykladowca.Role),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred =new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer,
                claims
                , expires: expires
                ,signingCredentials: cred);

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}
