using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class WorkTimeLogRepository(IPlanSphereDatabaseContext context, ILogger<WorkTimeLogRepository> logger) : IWorkTimeLogRepository
{
    private readonly IPlanSphereDatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<WorkTimeLogRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<WorkTimeLog> CreateAsync(WorkTimeLog request, CancellationToken cancellationToken)
    {
        _context.WorkTimeLogs.Add(request);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public Task<WorkTimeLog> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<WorkTimeLog> UpdateAsync(ulong id, WorkTimeLog request, CancellationToken cancellationToken)
    {
        _context.WorkTimeLogs.Update(request);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public Task<WorkTimeLog> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<WorkTimeLog> GetQueryable()
    {
        throw new NotImplementedException();
    }

    public async Task<WorkTimeLog> GetUncheckedLogAsync(ulong userId, CancellationToken cancellationToken)
    {
        var workTimeLog = await _context.WorkTimeLogs.SingleOrDefaultAsync(x => x.UserId == userId && x.StartDateTime.Date == DateTime.Today && x.EndDateTime == null, cancellationToken);
        if (workTimeLog == null)
        {
            _logger.LogInformation("No work time log found that does not have either start and end time set today!");
            throw new KeyNotFoundException("Is already checked out!");
        }

        return workTimeLog;
    }
}