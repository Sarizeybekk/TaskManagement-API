using Xunit;
using Moq;
using TaskManagement.Business.Services;
using TaskManagement.Data.Context;
using TaskManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Tests
{
    public class UserServiceTests : IDisposable
    {
        private readonly UserService _userService;
        private readonly AppDbContext _context;
        private readonly Mock<FluentValidation.IValidator<User>> _validatorMock;

        public UserServiceTests()
        {
            // InMemoryDatabase Setup
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new AppDbContext(options);

            // Validator Mock
            _validatorMock = new Mock<FluentValidation.IValidator<User>>();
            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<User>(), default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            // Service Instance
            _userService = new UserService(_context, _validatorMock.Object);
        }

        [Fact]
        public async Task CreateUser_ShouldAddUserToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            // Act
            var result = await _userService.CreateUserAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal("john.doe@example.com", result.Email);
            Assert.Equal(1, await _context.Users.CountAsync());
        }

        [Fact]
        public async Task CreateUser_ShouldThrowException_WhenEmailAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var existingUser = new User
            {
                Name = "Existing User",
                Email = "existing@example.com"
            };
            _context.Users.Add(existingUser);
            await _context.SaveChangesAsync();

            var newUser = new User
            {
                Name = "New User",
                Email = "existing@example.com"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.CreateUserAsync(newUser));
            Assert.Equal("A user with the same email already exists.", exception.Message);
        }

        [Fact]
        public async Task CreateUser_ShouldThrowValidationException_WhenNameIsEmpty()
        {
            // Arrange
            var user = new User
            {
                Name = string.Empty,
                Email = "john.doe@example.com"
            };

            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<User>(), default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult
                {
                    Errors = { new FluentValidation.Results.ValidationFailure("Name", "Name is required.") }
                });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => _userService.CreateUserAsync(user));
            Assert.Contains("Name is required.", exception.Message);
        }

        [Fact]
        public async Task CreateUser_ShouldThrowValidationException_WhenEmailIsInvalid()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "invalid-email"
            };

            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<User>(), default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult
                {
                    Errors = { new FluentValidation.Results.ValidationFailure("Email", "Invalid email format.") }
                });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => _userService.CreateUserAsync(user));
            Assert.Contains("Invalid email format.", exception.Message);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var user1 = new User { Name = "User1", Email = "user1@example.com" };
            var user2 = new User { Name = "User2", Email = "user2@example.com" };

            _context.Users.Add(user1);
            _context.Users.Add(user2);
            await _context.SaveChangesAsync();

            // Act
            var users = await _userService.GetAllUsersAsync();

            // Assert
            Assert.Equal(2, users.Count());
            Assert.Contains(users, u => u.Email == "user1@example.com");
            Assert.Contains(users, u => u.Email == "user2@example.com");
        }

        public void Dispose()
        {
            // Clean up the database after each test
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
