using FluentValidation;
using PlanSphere.Core.Features.Organisations.Validators;

namespace PlanSphere.Core.Features.Organisations.Commands.UpdateOrganisation;

public class UpdateOrganisationCommandValidator : AbstractValidator<UpdateOrganisationCommand> 
{
    private readonly OrganisationUpdateRequestValidator _validator;
    public UpdateOrganisationCommandValidator(OrganisationUpdateRequestValidator validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));

        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.OrganisationUpdateRequest).SetValidator(_validator);
    }
}