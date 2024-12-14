using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interface.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<IEnumerable<User>> GetAllUsersAsync();
}