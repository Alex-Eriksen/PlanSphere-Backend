using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Features.WorkTimes.Commands.CheckInWorkTime;
using PlanSphere.Core.Features.WorkTimes.Commands.CheckOutWorkTime;
using PlanSphere.Core.Interfaces.Repositories;
using Quartz;

namespace PlanSphere.CronJobScheduler.Jobs;

public class CheckInAndOutJob(
    ILogger<CheckInAndOutJob> logger,
    IWorkScheduleRepository workScheduleRepository,
    IWorkTimeRepository workTimeRepository,
    IMediator mediator
) : ScheduledJobBase(logger)
{
    public static JobKey JobKey => _jobKey ??= new JobKey(nameof(CheckInAndOutJob));
    private static JobKey? _jobKey;
    
    private readonly ILogger<CheckInAndOutJob> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IWorkScheduleRepository _workScheduleRepository = workScheduleRepository ?? throw new ArgumentNullException(nameof(workScheduleRepository));
    private readonly IWorkTimeRepository _workTimeRepository = workTimeRepository ?? throw new ArgumentNullException(nameof(workTimeRepository));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    protected override async Task RunJobAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var nowTimeOnly = TimeOnly.FromDateTime(now);
        var windowStart = nowTimeOnly.AddMinutes(-5);
        var windowEnd = nowTimeOnly.AddMinutes(5);
        
        _logger.LogInformation("CheckInAndOutJob started at {Time}", now);

        var workSchedules = _workScheduleRepository.GetQueryable()
            .Where(ws => ws.UserSettings.AutoCheckInOut &&
                         ws.WorkScheduleShifts.Any(shift =>
                             shift.Day == now.DayOfWeek && 
                             (shift.StartTime.IsBetween(windowStart, windowEnd) ||
                              shift.EndTime.IsBetween(windowStart, windowEnd))))
            .ToList();

        foreach (var workSchedule in workSchedules)
        {
            var userId = workSchedule.UserSettings.UserId;

            var shifts = workSchedule.WorkScheduleShifts
                .Where(shift => 
                    shift.Day == now.DayOfWeek && 
                    (shift.StartTime.IsBetween(windowStart, windowEnd) || shift.EndTime.IsBetween(windowStart, windowEnd)));
            
            foreach (var shift in shifts)
            {
                if (shift.StartTime >= windowStart && shift.StartTime <= windowEnd)
                {
                    await CheckInAsync(userId, cancellationToken);
                }

                if (shift.EndTime >= windowStart && shift.EndTime <= windowEnd)
                {
                    await CheckOutAsync(userId, cancellationToken);
                }
            }
        }
    }
    
    public static void Configure(IServiceCollectionQuartzConfigurator configurator)
    {
        configurator.AddJob<CheckInAndOutJob>(JobKey)
            .AddTrigger(trigger =>
            {
                trigger.ForJob(JobKey)
                    .WithCronSchedule("0 */1 * * * ?");
            });
    }

    private async Task CheckInAsync(ulong userId, CancellationToken cancellationToken)
    {
        var workTime = await _workTimeRepository.GetWorkTimeTodayAsync(userId, cancellationToken);
        if (workTime != null)
        {
            _logger.LogInformation("User with id: [{userId}] is already checked in!", userId);
            return;
        }
        var checkInCommand = new CheckInWorkTimeCommand(userId);
        await _mediator.Send(checkInCommand, cancellationToken);
    }
    
    private async Task CheckOutAsync(ulong userId, CancellationToken cancellationToken)
    {
        var workTime = await _workTimeRepository.GetWorkTimeTodayAsync(userId, cancellationToken);
        if (workTime is not { EndDateTime: null })
        {
            _logger.LogInformation("User with id: [{userId}] is already checked out!", userId);
            return;
        }
        var checkOutCommand = new CheckOutWorkTimeCommand(userId);
        await _mediator.Send(checkOutCommand, cancellationToken);
    }
}