using FluentValidation;
using PlanSphere.Core.Constants;

namespace PlanSphere.Core.Features.Teams.Commands.CreateTeam;

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .MaximumLength(FieldLengthConstants.Short);
        RuleFor(x => x.Request.Description)
            .MaximumLength(FieldLengthConstants.Long);
        RuleFor(x => x.Request.Identifier)
            .MaximumLength(FieldLengthConstants.Short);
    }
}