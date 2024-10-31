using FluentValidation;
using PlanSphere.Core.Features.JobTitles.Validators;

namespace PlanSphere.Core.Features.JobTitles.Commands.CreateJobTitle;

public class CreateJobTitleCommandValidator : AbstractValidator<CreateJobTitleCommand>
{
    private readonly JobTitleRequestValidator _validator;
    public CreateJobTitleCommandValidator(JobTitleRequestValidator validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));

        RuleFor(x => x.SourceLevel)
            .NotNull()
            .IsInEnum();

        RuleFor(x => x.SourceLevelId)
            .NotNull();

        RuleFor(x => x.Request)
            .SetValidator(_validator);
    }
}