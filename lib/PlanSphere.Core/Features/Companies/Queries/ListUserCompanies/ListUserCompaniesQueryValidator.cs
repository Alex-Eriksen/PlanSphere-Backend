using FluentValidation;

namespace PlanSphere.Core.Features.Companies.Queries.ListUserCompanies;

public class ListUserCompaniesQueryValidator : AbstractValidator<ListUserCompaniesQuery>
{
    public ListUserCompaniesQueryValidator()
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