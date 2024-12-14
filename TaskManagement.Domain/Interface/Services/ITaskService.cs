
using TaskManagement.Domain.Entities;
using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Domain.Interface.Services
{

    public interface ITaskService
    {
        Task<Task> CreateTaskAsync(Task task);
        Task<IEnumerable<Task>> GetAllTasksAsync();
        Task<IEnumerable<Task>> GetTasksByUserIdAsync(int userId);
        System.Threading.Tasks.Task CompleteTaskAsync(int taskId);
    }
}