using FluentValidation;
using PlanSphere.Core.Constants;

namespace PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;

public class CreateOrganisationValidator : AbstractValidator<CreateOrganisationCommand>
{
    public CreateOrganisationValidator()
    {
        RuleFor(command => command.Request.Name)
            .NotEmpty()
            .MaximumLength(FieldLengthConstants.Short);
        
        RuleFor(command => command.Request.Address)
            .NotEmpty();
    }
}