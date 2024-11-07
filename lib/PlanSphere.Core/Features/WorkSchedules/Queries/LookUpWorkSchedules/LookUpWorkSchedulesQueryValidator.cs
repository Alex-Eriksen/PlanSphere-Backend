using FluentValidation;

namespace PlanSphere.Core.Features.WorkSchedules.Queries.LookUpWorkSchedules;

public class LookUpWorkSchedulesQueryValidator : AbstractValidator<LookUpWorkSchedulesQuery>
{
    public LookUpWorkSchedulesQueryValidator()
    {
        RuleFor(x => x.UserId).NotNull();
    }
}