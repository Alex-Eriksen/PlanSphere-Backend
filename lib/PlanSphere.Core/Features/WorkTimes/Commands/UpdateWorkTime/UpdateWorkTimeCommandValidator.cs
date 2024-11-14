using FluentValidation;
using PlanSphere.Core.Features.WorkTimes.Validators;

namespace PlanSphere.Core.Features.WorkTimes.Commands.UpdateWorkTime;

public class UpdateWorkTimeCommandValidator : AbstractValidator<UpdateWorkTimeCommand>
{
    private WorkTimeRequestValidator _requestValidator;
    public UpdateWorkTimeCommandValidator(WorkTimeRequestValidator requestValidator)
    {
        _requestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator));
        RuleFor(x => x.UserId)
            .NotNull();
        
        RuleFor(x => x.WorkTimeId)
            .NotNull();
        
        RuleFor(x => x.ActionType)
            .NotNull();

        RuleFor(x => x.Request)
            .SetValidator(_requestValidator);
    }
}