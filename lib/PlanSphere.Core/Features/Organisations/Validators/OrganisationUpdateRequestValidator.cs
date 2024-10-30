using FluentValidation;
using PlanSphere.Core.Features.Organisations.Requests;

namespace PlanSphere.Core.Features.Organisations.Validators;

public class OrganisationUpdateRequestValidator : AbstractValidator<OrganisationUpdateRequest>
{
    public OrganisationUpdateRequestValidator()
    {
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.Address).NotNull();
        RuleFor(x => x.Settings).NotNull();
    }
}