using FluentValidation;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Extensions.ValidationExtensions;
using PlanSphere.Core.Features.Teams.Request;

namespace PlanSphere.Core.Features.Teams.Commands.PatchTeam;

public class PatchTeamCommandValidator : AbstractValidator<PatchTeamCommand>
{
    public PatchTeamCommandValidator()
    {
        RuleFor(x => x.PatchDocument)
            .NotNull();
        RuleFor(x => x.TeamId)
            .NotNull();
        RuleForEach(x => x.PatchDocument.Operations)
            .AllowOperators(PatchOperators.REPLACE)
            .NotNull(nameof(TeamPatchRequest.Name))
            .NotNull(nameof(TeamPatchRequest.Description))
            .NotNull(nameof(TeamPatchRequest.Identifier));
    }
}