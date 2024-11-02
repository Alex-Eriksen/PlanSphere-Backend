using FluentValidation;

namespace PlanSphere.Core.Features.Organisations.Commands.DeleteOrganisation;

public class DeleteOrganisationCommandValidator : AbstractValidator<DeleteOrganisationCommand>
{
    public DeleteOrganisationCommandValidator()
    {
        RuleFor(x => x.OrganisationId)
            .NotNull();
    }
}