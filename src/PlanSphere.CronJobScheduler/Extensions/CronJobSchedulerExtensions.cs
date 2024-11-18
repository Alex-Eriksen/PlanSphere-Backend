using Microsoft.Extensions.DependencyInjection;
using PlanSphere.CronJobScheduler.Jobs;
using Quartz;

namespace PlanSphere.CronJobScheduler.Extensions;

public static class CronJobSchedulerExtensions
{
    public static IServiceCollection AddCronJobScheduler(this IServiceCollection services)
    {
        services.AddHostedService<QuartzBackgroundService>();
        services.AddQuartz(configurator =>
        {
            configurator.UseTimeZoneConverter();

            CheckInAndOutJob.Configure(configurator);
        });

        return services;
    }
}