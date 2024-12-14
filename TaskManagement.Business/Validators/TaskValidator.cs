using FluentValidation;
using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Business.Validators
{
    public class TaskValidator : AbstractValidator<Task>
    {
        public TaskValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.AssignedToUserId).GreaterThan(0).WithMessage("AssignedToUserId must be a valid user.");
        }
    }
}