using FluentValidation;
using PlanSphere.Core.Features.WorkTimes.Validators;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.WorkTimes.Commands.CreateWorkTime;

public class CreateWorkTimeCommandValidator : AbstractValidator<CreateWorkTimeCommand>
{
    private WorkTimeRequestValidator _requestValidator;
    public CreateWorkTimeCommandValidator(WorkTimeRequestValidator requestValidator)
    {
        _requestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator));
        RuleFor(x => x.UserId)
            .NotNull();

        RuleFor(x => x.Request)
            .SetValidator(_requestValidator);
    }
}