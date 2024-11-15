using FluentValidation;

namespace PlanSphere.Core.Features.WorkTimes.Commands.DeleteWorkTime;

public class DeleteWorkTimeCommandValidator : AbstractValidator<DeleteWorkTimeCommand>
{
    public DeleteWorkTimeCommandValidator()
    {
        RuleFor(x => x.WorkTimeId)
            .NotNull();
        
        RuleFor(x => x.UserId)
            .NotNull();
    }
}