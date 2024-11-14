using FluentValidation;
using PlanSphere.Core.Features.WorkTimes.Validators;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.WorkTimes.Commands.CreateWorkTime;

public class CreateWorkTimeCommandValidator : AbstractValidator<CreateWorkTimeCommand>
{
    private WorkTimeRequestValidator _requestValidator;
    private IWorkTimeRepository _workTimeRepository;
    public CreateWorkTimeCommandValidator(WorkTimeRequestValidator requestValidator, IWorkTimeRepository workTimeRepository)
    {
        _workTimeRepository = workTimeRepository ?? throw new ArgumentNullException(nameof(workTimeRepository));
        _requestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator));
        RuleFor(x => x.UserId)
            .NotNull()
            .MustAsync(async (userId, cancellationToken) =>
            {
                var hasBeenCreated = await _workTimeRepository.IsWorkTimeAlreadyCreatedTodayAsync(userId, cancellationToken);
                return !hasBeenCreated;
            }).WithMessage("User has already created a work time today.");

        RuleFor(x => x.ActionType)
            .NotNull();
        
        RuleFor(x => x.Request)
            .SetValidator(_requestValidator);
        
    }
}