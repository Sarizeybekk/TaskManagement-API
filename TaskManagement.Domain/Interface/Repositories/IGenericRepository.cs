using TaskManagement.Domain.Entities.Common;

namespace TaskManagement.Domain.Interface.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}