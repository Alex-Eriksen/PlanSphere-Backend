using FluentValidation;

namespace PlanSphere.Core.Features.Teams.Commands.DeleteTeam;

public class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
{
    public DeleteTeamCommandValidator()
    {
        RuleFor(x => x.TeamId).NotNull();
    }
}