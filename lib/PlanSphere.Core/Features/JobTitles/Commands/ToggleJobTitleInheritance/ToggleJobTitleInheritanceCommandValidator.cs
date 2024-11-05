using Domain.Entities.EmbeddedEntities;
using FluentValidation;

namespace PlanSphere.Core.Features.JobTitles.Commands.ToggleJobTitleInheritance;

public class ToggleJobTitleInheritanceCommandValidator : AbstractValidator<ToggleJobTitleInheritanceCommand>
{
    public ToggleJobTitleInheritanceCommandValidator()
    {
        RuleFor(x => x.JobTitleId)
            .NotNull();

        RuleFor(x => x.SourceLevel)
            .IsInEnum()
            .NotEqual(SourceLevel.Team)
            .WithMessage("You can disable inheritance on team level.");
    }
}