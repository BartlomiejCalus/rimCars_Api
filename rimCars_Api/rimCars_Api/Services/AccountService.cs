using AutoMapper;
using Microsoft.AspNetCore.Identity;
using rimCars_Api.Entities;
using rimCars_Api.Models;
using rimCars_Api.Exceptations;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace rimCars_Api.Services
{
    public interface IAccountService
    {
        string GenerateJwt(LoginUserDto dto);
        void RegisterUser(RegisterUserDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly SalonsDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHaser;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(SalonsDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHaser = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                RoleId = dto.RoleId,
            };

            var passwordHash = _passwordHaser.HashPassword(newUser, dto.Password);
            newUser.PasswordHs = passwordHash;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }

        public string GenerateJwt(LoginUserDto dto)
        {
            var user = _dbContext.Users
                .Include(u=>u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);

            if(user is null)
            {
                throw new BadRequestExceptation("Invalid email or password");
            }

            var result = _passwordHaser.VerifyHashedPassword(user, user.PasswordHs, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestExceptation("Invalid email or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role}"),
                new Claim("Company", $"{user.Company}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer
                , _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
