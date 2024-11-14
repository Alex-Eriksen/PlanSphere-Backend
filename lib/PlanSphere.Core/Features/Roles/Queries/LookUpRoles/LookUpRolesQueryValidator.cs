using FluentValidation;

namespace PlanSphere.Core.Features.Roles.Queries.LookUpRoles;

public class LookUpRolesQueryValidator : AbstractValidator<LookUpRolesQuery>
{
    public LookUpRolesQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull();
    }
}