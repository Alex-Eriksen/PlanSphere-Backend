using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.WorkTimes.Commands.CheckInWorkTime;

[HandlerType(HandlerType.SystemApi)]
public class CheckInWorkTimeCommandHandler(
    IWorkTimeRepository workTimeRepository,
    IUserRepository userRepository,
    IWorkTimeLogRepository workTimeLogRepository,
    ILogger<CheckInWorkTimeCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<CheckInWorkTimeCommand>
{
    private readonly IWorkTimeRepository _workTimeRepository = workTimeRepository ?? throw new ArgumentNullException(nameof(workTimeRepository));
    private readonly IWorkTimeLogRepository _workTimeLogRepository = workTimeLogRepository ?? throw new ArgumentNullException(nameof(workTimeLogRepository));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly ILogger<CheckInWorkTimeCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task Handle(CheckInWorkTimeCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("User is checking in");
        
        _logger.LogInformation("Fetching user settings from user with id: [{userId}]", request.UserId);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        var shift = user.Settings.WorkSchedule.WorkScheduleShifts.SingleOrDefault(x => x.Day == DateTime.Now.DayOfWeek, null);
        _logger.LogInformation("Fetched user settings from user with id: [{userId}]", request.UserId);
        
        _logger.LogInformation("Fetching any existing work times created today with unset end time, from user with id: [{userId}]", request.UserId);
        var previousWorkTime = await _workTimeRepository.GetWorkTimeTodayAsync(request.UserId, cancellationToken);
        _logger.LogInformation("Fetched existing work times created today with unset end time, from user with id: [{userId}]", request.UserId);

        if (previousWorkTime is { EndDateTime: null })
        {
            _logger.LogInformation("User is trying to check in before checking out their previous work time");
            throw new InvalidOperationException("You must check out first before checking in again!");
        }
        
        var workTime = new WorkTime()
        {
            UserId = request.UserId,
            Location = ShiftLocation.Office,
            WorkTimeType = WorkTimeType.Regular
        };
        
        if (previousWorkTime == null)
        {
            workTime = DetermineFirstCheckInWorkType(workTime, shift); // First check-in
        }
        else
        {
            workTime = DetermineOvertimeForSubsequentCheckIn(workTime, shift, previousWorkTime); // Subsequent check-ins
        }
        
        _logger.LogInformation("Checking in user with id: [{userId}]", request.UserId);
        workTime = await _workTimeRepository.CreateAsync(workTime, cancellationToken);
        _logger.LogInformation("Checked in user with id: [{userId}]", request.UserId);

        var workTimeLog = _mapper.Map<WorkTimeLog>(workTime);
        
        _logger.LogInformation("Creating a work time log for user with id: [{userId}].", request.UserId);
        await _workTimeLogRepository.CreateAsync(workTimeLog, cancellationToken);
        _logger.LogInformation("Created a work time log for user with id: [{userId}].", request.UserId);
    }

    private WorkTime DetermineFirstCheckInWorkType(WorkTime workTime, WorkScheduleShift? shift)
    {
        if (IsOvertimeForNoShift(shift) || IsOvertimeForEarlyCheckIn(shift))
        {
            workTime.WorkTimeType = WorkTimeType.Overtime;
            workTime.Location = shift == null ? ShiftLocation.Remote : shift.Location;
        }

        return workTime;
    }

    private WorkTime DetermineOvertimeForSubsequentCheckIn(WorkTime workTime, WorkScheduleShift? shift, WorkTime previousWorkTime)
    {
        if (IsOvertimeForNoShift(shift) || IsOvertimeForEndTimeMatch(shift, previousWorkTime))
        {
            workTime.WorkTimeType = WorkTimeType.Overtime;
            workTime.Location = shift == null ? ShiftLocation.Remote : ShiftLocation.Office;
        }

        return workTime;
    }

    private bool IsOvertimeForNoShift(WorkScheduleShift? shift) => shift == null;
    private bool IsOvertimeForEarlyCheckIn(WorkScheduleShift? shift) => shift != null && DateTime.Now.TimeOfDay < shift.StartTime.ToTimeSpan();
    private bool IsOvertimeForEndTimeMatch(WorkScheduleShift? shift, WorkTime previousWorkTime) => shift != null && previousWorkTime.EndDateTime.HasValue && previousWorkTime.EndDateTime.Value.TimeOfDay == shift.EndTime.ToTimeSpan();
}