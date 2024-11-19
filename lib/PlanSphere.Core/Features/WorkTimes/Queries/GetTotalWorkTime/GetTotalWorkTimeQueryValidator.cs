using FluentValidation;

namespace PlanSphere.Core.Features.WorkTimes.Queries.GetTotalWorkTime;

public class GetTotalWorkTimeQueryValidator : AbstractValidator<GetTotalWorkTimeQuery>
{
    public GetTotalWorkTimeQueryValidator()
    {
        RuleFor(x => x.WorkTimeType)
            .IsInEnum()
            .NotNull();
        
        RuleFor(x => x.UserId)
            .NotNull();
        
        RuleFor(x => x.Period)
            .IsInEnum()
            .NotNull();
    }
}