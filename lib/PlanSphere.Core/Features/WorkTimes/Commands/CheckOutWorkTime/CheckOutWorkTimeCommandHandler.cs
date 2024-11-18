using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.WorkTimes.Commands.CheckOutWorkTime;

[HandlerType(HandlerType.SystemApi)]
[HandlerType(HandlerType.TaskScheduler)]
public class CheckOutWorkTimeCommandHandler(
    IWorkTimeRepository workTimeRepository,
    IUserRepository userRepository,
    IWorkTimeLogRepository workTimeLogRepository,
    ILogger<CheckOutWorkTimeCommandHandler> logger
) : IRequestHandler<CheckOutWorkTimeCommand>
{
    private readonly IWorkTimeRepository _workTimeRepository = workTimeRepository ?? throw new ArgumentNullException(nameof(workTimeRepository));
    private readonly IWorkTimeLogRepository _workTimeLogRepository = workTimeLogRepository ?? throw new ArgumentNullException(nameof(workTimeLogRepository));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly ILogger<CheckOutWorkTimeCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Handle(CheckOutWorkTimeCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        var shift = user.Settings.WorkSchedule.WorkScheduleShifts.SingleOrDefault(x => x.Day == DateTime.UtcNow.DayOfWeek, null);
        
        var currentWorkTime = await _workTimeRepository.GetWorkTimeTodayAsync(request.UserId, cancellationToken);

        CheckIfExists(currentWorkTime, request.UserId);
        
        if (shift != null)
        {
            currentWorkTime.EndDateTime = DateTime.UtcNow;
            if (IsWithinShiftEndTime(shift))
            {
                var endDateTime = DateTime.Today;
                endDateTime += shift.EndTime.ToTimeSpan();
                currentWorkTime.EndDateTime = endDateTime;
            }
        }
        else
        {
            currentWorkTime.EndDateTime = DateTime.UtcNow;
        }

        _logger.LogInformation("Checking out work time for user with id: [{userId}]", request.UserId);
        await _workTimeRepository.UpdateAsync(currentWorkTime.Id, currentWorkTime, cancellationToken);
        _logger.LogInformation("Checked out work time for user with id: [{userId}]", request.UserId);

        _logger.LogInformation("Fetching log without end time!");
        var workTimeLog = await _workTimeLogRepository.GetUncheckedLogAsync(request.UserId, cancellationToken);
        _logger.LogInformation("Fetched log without end time!");

        workTimeLog.EndDateTime = currentWorkTime.EndDateTime;
        
        _logger.LogInformation("Updating log with end time!");
        await _workTimeLogRepository.UpdateAsync(workTimeLog.Id, workTimeLog, cancellationToken);
        _logger.LogInformation("Updated log with end time!");
    }

    private bool IsWithinShiftEndTime(WorkScheduleShift shift)
    {
        var fifteenMinutesBefore = shift.EndTime.AddMinutes(-15).ToTimeSpan();
        var fifteenMinutesAfter = shift.EndTime.AddMinutes(15).ToTimeSpan();
            
        var currentTime = DateTime.UtcNow.TimeOfDay;

        return currentTime >= fifteenMinutesBefore && currentTime <= fifteenMinutesAfter;
    }

    private void CheckIfExists(WorkTime? workTime, ulong userId)
    {
        if (workTime == null)
        {
            _logger.LogInformation("Work time could not be found!");
            throw new KeyNotFoundException("Work time could not be found!");
        }

        if (workTime.EndDateTime == null) return;
        _logger.LogInformation("User with id: [{userId}] has already checked out their current work time. Throwing exception", userId);
        throw new InvalidOperationException("Current work time is already checked out!");
    }
}
