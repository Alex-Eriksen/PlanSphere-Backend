using FluentValidation;

namespace PlanSphere.Core.Features.Departments.Queries.ListDepartments;

public class ListDepartmentQueryValidator : AbstractValidator<ListDepartmentQuery>
{
    public ListDepartmentQueryValidator()
    {
        RuleFor(x => x.CompanyId)
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