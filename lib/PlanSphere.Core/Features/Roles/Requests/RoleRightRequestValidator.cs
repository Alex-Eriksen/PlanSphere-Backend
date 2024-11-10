using FluentValidation;

namespace PlanSphere.Core.Features.Roles.Requests;

public class RoleRightRequestValidator : AbstractValidator<RoleRightRequest>
{
    public RoleRightRequestValidator()
    {
        RuleFor(x => x.SourceLevel).NotNull().IsInEnum();
        RuleFor(x => x.SourceLevelId).NotNull().NotEmpty();
        RuleFor(x => x.RightId).NotNull().NotEmpty();
    }
}