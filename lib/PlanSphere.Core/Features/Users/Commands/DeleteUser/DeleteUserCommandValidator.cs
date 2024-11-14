using FluentValidation;

namespace PlanSphere.Core.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.userId)
            .NotNull();
    }
    
}