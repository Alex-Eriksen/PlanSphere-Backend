using FluentValidation;

namespace PlanSphere.Core.Features.WorkSchedules.Queries.GetWorkScheduleById;

public class GetWorkScheduleByIdQueryValidator : AbstractValidator<GetWorkScheduleByIdQuery>
{
    public GetWorkScheduleByIdQueryValidator()
    {
        RuleFor(x => x.WorkScheduleId).NotNull();
    }
}