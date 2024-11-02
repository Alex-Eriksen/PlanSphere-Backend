using FluentValidation;

namespace PlanSphere.Core.Features.JobTitles.Commands.ToggleJobTitleInheritance;

public class ToggleJobTitleInheritanceCommandValidator : AbstractValidator<ToggleJobTitleInheritanceCommand>
{
    public ToggleJobTitleInheritanceCommandValidator()
    {
        RuleFor(x => x.JobTitleId)
            .NotNull();
    }
}