using FluentValidation;

namespace PlanSphere.Core.Features.WorkTimes.Queries.GetWorkTimes;

public class GetWorkTimesQueryValidator : AbstractValidator<GetWorkTimesQuery>
{
    public GetWorkTimesQueryValidator()
    {
        RuleFor(x => x.Month)
            .NotNull();

        RuleFor(x => x.Year)
            .NotNull();
        
        RuleFor(x => x.UserId)
            .NotNull();
    }
}