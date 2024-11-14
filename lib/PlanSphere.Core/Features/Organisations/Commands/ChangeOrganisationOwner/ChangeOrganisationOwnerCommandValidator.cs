using FluentValidation;

namespace PlanSphere.Core.Features.Organisations.Commands.ChangeOrganisationOwner;

public class ChangeOrganisationOwnerCommandValidator : AbstractValidator<ChangeOrganisationOwnerCommand>
{
    public ChangeOrganisationOwnerCommandValidator()
    {
        RuleFor(x => x.OrganisationId).NotNull();
        RuleFor(x => x.UserId).NotNull();
    }
}