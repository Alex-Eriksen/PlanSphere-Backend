using FluentValidation;

namespace PlanSphere.Core.Features.Teams.Queries.ListUserTeams;

public class ListUserTeamQueryValidator : AbstractValidator<ListUserTeamQuery>
{
    public ListUserTeamQueryValidator()
    {
        RuleFor(x => x.SortBy)
            .IsInEnum()
            .NotNull();

        RuleFor(x => x.SortDescending)
            .NotNull();

        RuleFor(x => x.PageNumber)
            .NotNull();

        RuleFor(x => x.PageSize)
            .NotNull();
    }
}