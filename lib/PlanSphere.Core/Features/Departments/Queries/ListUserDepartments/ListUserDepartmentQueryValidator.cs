using FluentValidation;

namespace PlanSphere.Core.Features.Departments.Queries.ListUserDepartments;

public class ListUserDepartmentQueryValidator : AbstractValidator<ListUserDepartmentQuery>
{
    public ListUserDepartmentQueryValidator()
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