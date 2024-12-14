
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interface.Services;
using FluentValidation;

namespace TaskManagement.Business.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IValidator<User> _validator;

        public UserService(AppDbContext context, IValidator<User> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var validationResult = await _validator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new Exception("A user with the same email already exists.");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }

}