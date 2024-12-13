using TaskManagement.Domain.Entities.Common;

namespace TaskManagement.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}