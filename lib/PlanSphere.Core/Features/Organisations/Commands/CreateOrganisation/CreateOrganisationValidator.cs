using FluentValidation;
using PlanSphere.Core.Constants;

namespace PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;

public class CreateOrganisationValidator : AbstractValidator<CreateOrganisationCommand>
{
    public CreateOrganisationValidator()
    {
        RuleFor(command => command.OrganisationName).NotEmpty().WithMessage("Name cannot be empty");
        RuleFor(command => command.OrganisationName).MaximumLength(FieldLengthConstants.Short);
        
        RuleFor(command => command.Address).NotEmpty().WithMessage("Address cannot be empty");
    }
}