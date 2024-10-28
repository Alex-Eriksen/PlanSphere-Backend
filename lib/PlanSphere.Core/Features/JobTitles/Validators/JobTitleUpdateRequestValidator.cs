using FluentValidation;
using PlanSphere.Core.Features.JobTitles.Requests;

namespace PlanSphere.Core.Features.JobTitles.Validators;

public class JobTitleUpdateRequestValidator : AbstractValidator<JobTitleUpdateRequest>
{
    public JobTitleUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull();

        RuleFor(x => x.IsInheritanceActive)
            .NotNull();
    }
}