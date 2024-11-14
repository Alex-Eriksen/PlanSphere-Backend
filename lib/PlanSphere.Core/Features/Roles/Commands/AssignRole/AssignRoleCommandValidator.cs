using FluentValidation;

namespace PlanSphere.Core.Features.Roles.Commands.AssignRole;

public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
{
    public AssignRoleCommandValidator()
    {
        RuleFor(x => x.RoleId).NotNull();
        RuleFor(x => x.UserId).NotNull();
    }
}