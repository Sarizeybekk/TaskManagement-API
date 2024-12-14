
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;

using FluentValidation;
using TaskManagement.Domain.Interface.Services;
using DomainTask = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Business.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;
        private readonly IValidator<DomainTask> _validator;

        public TaskService(AppDbContext context, IValidator<DomainTask> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<DomainTask> CreateTaskAsync(DomainTask task)
        {
            var validationResult = await _validator.ValidateAsync(task);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var userExists = await _context.Users.AnyAsync(u => u.Id == task.AssignedToUserId);
            if (!userExists)
                throw new Exception("AssignedToUserId must refer to a valid user.");

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async System.Threading.Tasks.Task<IEnumerable<DomainTask>> GetAllTasksAsync()
        {
            return await _context.Tasks
                .Include(t => t.AssignedToUser)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<IEnumerable<DomainTask>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.Tasks
                .Where(t => t.AssignedToUserId == userId)
                .ToListAsync();
        }

        public async Task CompleteTaskAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
                throw new Exception("Task not found.");

            task.IsCompleted = true;
            await _context.SaveChangesAsync();
        }

    }
}