using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class WorkScheduleRepository(IPlanSphereDatabaseContext context, ILogger<WorkScheduleRepository> logger) : IWorkScheduleRepository
{
    private readonly IPlanSphereDatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<WorkScheduleRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    public async Task<WorkSchedule> CreateAsync(WorkSchedule request, CancellationToken cancellationToken)
    {
        _context.WorkSchedules.Add(request);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public Task<WorkSchedule> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<WorkSchedule> UpdateAsync(ulong id, WorkSchedule request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<WorkSchedule> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<WorkSchedule> GetQueryable()
    {
        return _context.WorkSchedules.AsNoTracking().AsQueryable();
    }
}