using FluentValidation;

namespace PlanSphere.Core.Features.WorkSchedules.Commands.UpdateWorkSchedule;

public class UpdateWorkScheduleCommandValidator : AbstractValidator<UpdateWorkScheduleCommand>
{
    public UpdateWorkScheduleCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull();
    }
}