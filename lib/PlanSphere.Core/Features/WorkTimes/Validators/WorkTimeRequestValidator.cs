using FluentValidation;
using PlanSphere.Core.Features.WorkTimes.Requests;

namespace PlanSphere.Core.Features.WorkTimes.Validators;

public class WorkTimeRequestValidator : AbstractValidator<WorkTimeRequest>
{
    public WorkTimeRequestValidator()
    {
        RuleFor(x => x.StartDateTime)
            .NotNull();

        RuleFor(x => x.EndDateTime)
            .NotNull();

        RuleFor(x => x)
            .Must(request => request.EndDateTime == null|| request.StartDateTime <= request.EndDateTime)
            .WithMessage("Start time cannot be later than end time.");
        
        RuleFor(x => x.StartDateTime)
            .NotNull();
        
        RuleFor(x => x.WorkTimeType)
            .IsInEnum()
            .NotNull();
        
        RuleFor(x => x.Location)
            .IsInEnum()
            .NotNull();
    }
}