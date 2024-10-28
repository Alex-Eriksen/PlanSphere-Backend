using FluentValidation;
using PlanSphere.Core.Features.JobTitles.Requests;

namespace PlanSphere.Core.Features.JobTitles.Validators;

public class JobTitleRequestValidator : AbstractValidator<JobTitleRequest>
{
    private readonly JobTitleRequestValidator _validator;
    public JobTitleRequestValidator(JobTitleRequestValidator validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        RuleFor(x => x.Name)
            .NotNull();

        RuleFor(x => x.IsInheritanceActive)
            .NotNull();
    }
}