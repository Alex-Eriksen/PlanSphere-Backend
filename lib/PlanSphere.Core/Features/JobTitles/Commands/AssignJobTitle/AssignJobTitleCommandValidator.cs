using FluentValidation;

namespace PlanSphere.Core.Features.JobTitles.Commands.AssignJobTitle;

public class AssignJobTitleCommandValidator : AbstractValidator<AssignJobTitleCommand>
{
    public AssignJobTitleCommandValidator()
    {
        RuleFor(x => x.JobTitleId).NotNull();
        RuleFor(x => x.UserId).NotNull();
    }
}