using FluentValidation;

namespace PlanSphere.Core.Features.WorkSchedules.Commands.CreateWorkSchedule;

public class CreateWorkScheduleCommandValidator : AbstractValidator<CreateWorkScheduleCommand>
{
    public CreateWorkScheduleCommandValidator()
    {
        RuleFor(x => x.Request.SourceLevelId)
            .NotNull();
    }
}