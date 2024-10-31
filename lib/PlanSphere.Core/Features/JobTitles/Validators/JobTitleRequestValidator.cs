using FluentValidation;
using PlanSphere.Core.Features.JobTitles.Requests;

namespace PlanSphere.Core.Features.JobTitles.Validators;

public class JobTitleRequestValidator : AbstractValidator<JobTitleRequest>
{
    public JobTitleRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull();

        RuleFor(x => x.IsInheritanceActive)
            .NotNull();
    }
}