using FluentValidation;

namespace PlanSphere.Core.Features.WorkTimes.Commands.CheckOutWorkTime;

public class CheckOutWorkTimeCommandValidator : AbstractValidator<CheckOutWorkTimeCommand>
{
    public CheckOutWorkTimeCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull();
    }
}
