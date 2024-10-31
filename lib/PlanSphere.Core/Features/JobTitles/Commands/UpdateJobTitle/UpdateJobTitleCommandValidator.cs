using FluentValidation;
using PlanSphere.Core.Features.JobTitles.Validators;

namespace PlanSphere.Core.Features.JobTitles.Commands.UpdateJobTitle;

public class UpdateJobTitleCommandValidator : AbstractValidator<UpdateJobTitleCommand>
{
    private readonly JobTitleRequestValidator _validator;
    public UpdateJobTitleCommandValidator(JobTitleRequestValidator validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));

        RuleFor(x => x.Id)
            .NotNull();
        
        RuleFor(x => x.SourceLevel)
            .NotNull()
            .IsInEnum();
        
        RuleFor(x => x.SourceLevelId)
            .NotNull();

        RuleFor(x => x.Request)
            .SetValidator(_validator);
    }
}