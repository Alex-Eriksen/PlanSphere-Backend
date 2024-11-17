using Microsoft.Extensions.Hosting;
using Quartz;

namespace PlanSphere.CronJobScheduler;

public class QuartzBackgroundService(ISchedulerFactory schedulerFactory) : BackgroundService
{
    private readonly ISchedulerFactory _schedulerFactory = schedulerFactory ?? throw new ArgumentNullException(nameof(schedulerFactory));
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scheduler = await _schedulerFactory.GetScheduler(stoppingToken);
        await scheduler.Start(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }

        await scheduler.Shutdown(stoppingToken);
    }
}