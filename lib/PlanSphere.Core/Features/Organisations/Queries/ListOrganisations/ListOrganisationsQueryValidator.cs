using FluentValidation;

namespace PlanSphere.Core.Features.Organisations.Queries.ListOrganisations;

public class ListOrganisationsQueryValidator : AbstractValidator<ListOrganisationsQuery>
{
    public ListOrganisationsQueryValidator()
    {
        // RuleFor(x => x.Search)
        //     .NotNull();
        //
        // RuleFor(x => x.SortBy)
        //     .IsInEnum()
        //     .NotNull();
        //
        // RuleFor(x => x.SortDescending)
        //     .NotNull();
    }
}