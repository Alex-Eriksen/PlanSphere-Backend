using FluentValidation;

namespace PlanSphere.Core.Features.Companies.Queries.ListCompanies;

public class ListCompaniesQueryValidator : AbstractValidator<ListCompaniesQuery>
{
    public ListCompaniesQueryValidator()
    {
        RuleFor(x => x.OrganisationId)
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