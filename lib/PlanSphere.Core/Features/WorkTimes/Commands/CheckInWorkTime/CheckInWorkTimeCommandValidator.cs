using FluentValidation;

namespace PlanSphere.Core.Features.WorkTimes.Commands.CheckInWorkTime;

public class CheckInWorkTimeCommandValidator : AbstractValidator<CheckInWorkTimeCommand>
{
    public CheckInWorkTimeCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull();
    }
}