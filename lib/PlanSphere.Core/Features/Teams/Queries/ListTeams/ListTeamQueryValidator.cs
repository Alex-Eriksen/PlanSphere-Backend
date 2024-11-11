using FluentValidation;
using PlanSphere.Core.Features.Departments.Queries.ListDepartments;

namespace PlanSphere.Core.Features.Teams.Queries.ListTeams;

public class ListTeamQueryValidator : AbstractValidator<ListTeamQuery>
{
    public ListTeamQueryValidator()
    {
        RuleFor(x => x.DepartmentId)
            .NotNull();
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