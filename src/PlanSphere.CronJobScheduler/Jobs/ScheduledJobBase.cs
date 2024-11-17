using Microsoft.Extensions.Logging;
using Quartz;

namespace PlanSphere.CronJobScheduler.Jobs;

public abstract class ScheduledJobBase(ILogger<ScheduledJobBase> logger) : IJob
{
    private readonly ILogger<ScheduledJobBase> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    public async Task Execute(IJobExecutionContext context)
    {
        using var _ = _logger.BeginScope("ScheduledJob {@JobDetail} with {JobID}", context.JobDetail, Guid.NewGuid().ToString());
        try
        {
            context.CancellationToken.ThrowIfCancellationRequested();
            await RunJobAsync(context.CancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured executing scheduled job");
            throw new JobExecutionException(msg: "", refireImmediately: false, cause: ex);
        }
        finally
        {
            _logger.LogInformation("Job ran for {JobRunTime}", context.JobRunTime);
        }
    }

    protected abstract Task RunJobAsync(CancellationToken cancellationToken);
}