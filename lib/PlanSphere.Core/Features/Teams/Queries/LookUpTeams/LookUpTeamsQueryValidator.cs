using FluentValidation;

namespace PlanSphere.Core.Features.Teams.Queries.LookUpTeams;

public class LookUpTeamsQueryValidator : AbstractValidator<LookUpTeamsQuery>
{
    public LookUpTeamsQueryValidator()
    {
        RuleFor(x => x.UserId).NotNull();
    }
}