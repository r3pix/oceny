using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using oceny5._0.Entities;
using oceny5._0.Exceptions;
using oceny5._0.Models;

namespace oceny5._0.Services
{
    public interface IStudentService
    {
        Task<int> Create(CreateStudentDto dto);
        Task<string> GenerateJwtKey(LoginStudentDto dto);
    }

    public class StudentService : IStudentService
    {
        private readonly OcenyDBContext _dbContext;
        private readonly IPasswordHasher<Student> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;

        public StudentService(OcenyDBContext dbContext, IPasswordHasher<Student> passwordHasher, IMapper mapper, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<int> Create(CreateStudentDto dto)
        {
            var student = _mapper.Map<Student>(dto);
            student.HashedPassword = _passwordHasher.HashPassword(student, dto.Password);

            _dbContext.Studenci.Add(student);
            await _dbContext.SaveChangesAsync();
            return student.Id;

        }

        public async Task<string> GenerateJwtKey(LoginStudentDto dto)
        {
            var student = await _dbContext.Studenci.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (student is null)
            {
                throw new BadRequestException("Bad credentials");
            }

            var verificationResult =
                _passwordHasher.VerifyHashedPassword(student, student.HashedPassword, dto.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Bad Credentials");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,student.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{student.Imie} {student.Nazwisko}"),
                new Claim(ClaimTypes.Role,student.Role)
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
