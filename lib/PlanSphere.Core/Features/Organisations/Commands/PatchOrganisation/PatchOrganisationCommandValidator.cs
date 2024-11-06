using FluentValidation;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Extensions.ValidationExtensions;
using PlanSphere.Core.Features.Organisations.Requests;

namespace PlanSphere.Core.Features.Organisations.Commands.PatchOrganisation;

public class PatchOrganisationCommandValidator : AbstractValidator<PatchOrganisationCommand>
{
    public PatchOrganisationCommandValidator()
    {
        RuleFor(x => x.PatchDocument)
            .NotNull();
        RuleFor(x => x.OrganisationId)
            .NotNull();

        RuleForEach(x => x.PatchDocument.Operations)
            .AllowOperators(PatchOperators.REPLACE)
            .NotNull(nameof(OrganisationUpdateRequest.Name))
            .NotNull(nameof(OrganisationUpdateRequest.Address));
    }
}