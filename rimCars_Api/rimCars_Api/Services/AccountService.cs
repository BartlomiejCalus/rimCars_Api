using AutoMapper;
using Microsoft.AspNetCore.Identity;
using rimCars_Api.Entities;
using rimCars_Api.Models;

namespace rimCars_Api.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly SalonsDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHaser;

        public AccountService(SalonsDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHaser = passwordHasher;
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
    }
}
