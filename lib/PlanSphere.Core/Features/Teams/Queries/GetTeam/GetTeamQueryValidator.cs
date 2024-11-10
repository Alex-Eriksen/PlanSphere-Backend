using FluentValidation;

namespace PlanSphere.Core.Features.Teams.Queries.GetTeam;

public class GetTeamQueryValidator : AbstractValidator<GetTeamQuery>
{
    public GetTeamQueryValidator()
    {
        RuleFor(x => x.TeamId)
            .NotNull();
    }
}