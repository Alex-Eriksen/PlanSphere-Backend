using FluentValidation;

namespace PlanSphere.Core.Features.JobTitles.Queries.ListJobTitles;

public class ListJobTitlesQueryValidator : AbstractValidator<ListJobTitlesQuery>
{
    public ListJobTitlesQueryValidator()
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