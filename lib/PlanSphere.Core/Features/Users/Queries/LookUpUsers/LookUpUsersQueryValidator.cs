using FluentValidation;

namespace PlanSphere.Core.Features.Users.Queries.LookUpUsers;

public class LookUpUsersQueryValidator : AbstractValidator<LookUpUsersQuery>
{
    public LookUpUsersQueryValidator()
    {
        RuleFor(x => x.OrganisationId).NotNull();
    }
}