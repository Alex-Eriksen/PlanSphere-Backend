using FluentValidation;

namespace PlanSphere.Core.Features.Roles.Commands.DeleteRole;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.RoleId).NotNull();
    }
}