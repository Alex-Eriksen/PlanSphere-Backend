using FluentValidation;

namespace PlanSphere.Core.Features.Organisations.Queries.GetOrganisationDetails;

public class GetOrganisationDetailsQueryValidator : AbstractValidator<GetOrganisationDetailsQuery>
{
    public GetOrganisationDetailsQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
    }
}