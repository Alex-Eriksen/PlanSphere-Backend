using FluentValidation;
using PlanSphere.Core.Features.Organisations.Validators;

namespace PlanSphere.Core.Features.Organisations.Commands.UpdateOrganisation;

public class UpdateOrganisationCommandValidator : AbstractValidator<UpdateOrganisationCommand> 
{
    private readonly OrganisationRequestValidator _validator;
    public UpdateOrganisationCommandValidator(OrganisationRequestValidator validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));

        RuleFor(x => x.OrganisationId).NotNull();
        //RuleFor(x => x.OrganisationRequest).SetValidator(_validator);
    }
}