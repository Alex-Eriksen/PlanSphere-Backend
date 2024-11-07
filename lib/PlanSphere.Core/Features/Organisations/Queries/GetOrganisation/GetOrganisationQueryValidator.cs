using FluentValidation;

namespace PlanSphere.Core.Features.Organisations.Queries.GetOrganisation;

public class GetOrganisationQueryValidator : AbstractValidator<GetOrganisationQuery>
{
    public GetOrganisationQueryValidator()
    {
        RuleFor(x => x.SourceLevelId)
            .NotNull();
    }
}