using TaskManagement.Domain.Entities.Common;

namespace TaskManagement.Domain.Entities
{

    public class Task : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int AssignedToUserId { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;


        public User? AssignedToUser { get; set; }
    }
}