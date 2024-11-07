using FluentValidation;
using PlanSphere.Core.Features.Companies.Validators;
using PlanSphere.Core.Features.JobTitles.Validators;
using PlanSphere.Core.Features.Organisations.Requests;
using PlanSphere.Core.Features.Roles.Validators;

namespace PlanSphere.Core.Features.Organisations.Validators;

public class OrganisationRequestValidator : AbstractValidator<OrganisationRequest>
{
    public OrganisationRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.Address)
            .NotNull();
        
        RuleFor(x => x.Settings)
            .NotNull();
    }
}