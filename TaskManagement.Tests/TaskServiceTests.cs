using Xunit;
using Moq;
using TaskManagement.Business.Services;
using TaskManagement.Data.Context;
using TaskManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Tests
{
    public class TaskServiceTests : IDisposable
    {
        private readonly TaskService _taskService;
        private readonly AppDbContext _context;

        public TaskServiceTests()
        {
            // InMemoryDatabase Setup
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TaskTestDatabase")
                .Options;

            _context = new AppDbContext(options);

            // Service Instance
            _taskService = new TaskService(_context, null); // Validator dependency is passed as null for simplicity
        }

        [Fact]
        public async Task CreateTask_ShouldAddTaskToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var task = new TaskManagement.Domain.Entities.Task
            {
                Title = "Test Task",
                Description = "This is a test task.",
                AssignedToUserId = 1,
                IsCompleted = false
            };

            // Act
            var result = await _taskService.CreateTaskAsync(task);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Task", result.Title);
            Assert.False(result.IsCompleted);
            Assert.Equal(1, await _context.Tasks.CountAsync());
        }

        [Fact]
        public async Task CreateTask_ShouldThrowException_WhenAssignedUserDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var task = new TaskManagement.Domain.Entities.Task
            {
                Title = "Invalid Task",
                AssignedToUserId = 99 // Non-existing User ID
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _taskService.CreateTaskAsync(task));
            Assert.Equal("Assigned user does not exist.", exception.Message);
        }

        [Fact]
        public async Task GetAllTasks_ShouldReturnAllTasks()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var task1 = new TaskManagement.Domain.Entities.Task { Title = "Task 1", AssignedToUserId = 1 };
            var task2 = new TaskManagement.Domain.Entities.Task { Title = "Task 2", AssignedToUserId = 2 };

            _context.Tasks.AddRange(task1, task2);
            await _context.SaveChangesAsync();

            // Act
            var tasks = await _taskService.GetAllTasksAsync();

            // Assert
            Assert.Equal(2, tasks.Count());
            Assert.Contains(tasks, t => t.Title == "Task 1");
            Assert.Contains(tasks, t => t.Title == "Task 2");
        }

        [Fact]
        public async Task GetTasksByUserId_ShouldReturnTasksForSpecificUser()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var task1 = new TaskManagement.Domain.Entities.Task { Title = "Task 1", AssignedToUserId = 1 };
            var task2 = new TaskManagement.Domain.Entities.Task { Title = "Task 2", AssignedToUserId = 2 };

            _context.Tasks.AddRange(task1, task2);
            await _context.SaveChangesAsync();

            // Act
            var tasks = await _taskService.GetTasksByUserIdAsync(1);

            // Assert
            Assert.Single(tasks);
            Assert.Contains(tasks, t => t.AssignedToUserId == 1);
        }

        [Fact]
        public async Task CompleteTask_ShouldMarkTaskAsCompleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var task = new TaskManagement.Domain.Entities.Task
            {
                Title = "Incomplete Task",
                AssignedToUserId = 1,
                IsCompleted = false
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Act
            await _taskService.CompleteTaskAsync(task.Id);
            var updatedTask = await _context.Tasks.FindAsync(task.Id);

            // Assert
            Assert.NotNull(updatedTask);
            Assert.True(updatedTask.IsCompleted);
        }

        [Fact]
        public async Task CompleteTask_ShouldThrowException_WhenTaskDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _taskService.CompleteTaskAsync(99));
            Assert.Equal("Task not found.", exception.Message);
        }

        public void Dispose()
        {
            // Clean up the database after each test
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
