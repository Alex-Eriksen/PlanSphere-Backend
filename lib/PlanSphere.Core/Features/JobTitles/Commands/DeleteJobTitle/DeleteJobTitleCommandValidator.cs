using FluentValidation;

namespace PlanSphere.Core.Features.JobTitles.Commands.DeleteJobTitle;

public class DeleteJobTitleCommandValidator : AbstractValidator<DeleteJobTitleCommand>
{
    public DeleteJobTitleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
        
        RuleFor(x => x.SourceLevel)
            .NotNull()
            .IsInEnum();

        RuleFor(x => x.SourceLevelId)
            .NotNull();
    }
}