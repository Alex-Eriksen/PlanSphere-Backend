using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class WorkTimeRepository(IPlanSphereDatabaseContext context, ILogger<WorkTimeRepository> logger) : IWorkTimeRepository
{
    private readonly IPlanSphereDatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<WorkTimeRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<WorkTime> CreateAsync(WorkTime request, CancellationToken cancellationToken)
    {
        _context.WorkTimes.Add(request);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<WorkTime> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        var workTime = await _context.WorkTimes.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (workTime == null)
        {
            _logger.LogInformation("Work time could not be found with the id: [{workTimeId}]. Work time doesn't exist!", id);
            throw new KeyNotFoundException($"Work time could not be found with the id: [{id}]. Work time doesn't exist!");
        }

        return workTime;
    }

    public async Task<WorkTime> UpdateAsync(ulong id, WorkTime request, CancellationToken cancellationToken)
    {
        _context.WorkTimes.Update(request);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<WorkTime> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        var workTime = await _context.WorkTimes.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (workTime == null)
        {
            _logger.LogInformation("Work time could not be found with the id: [{workTimeId}]. Work time doesn't exist!", id);
            throw new KeyNotFoundException($"Work time could not be found with the id: [{id}]. Work time doesn't exist!");
        }

        _context.WorkTimes.Remove(workTime);
        await _context.SaveChangesAsync(cancellationToken);
        return workTime;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<WorkTime> GetQueryable()
    {
        return _context.WorkTimes.AsNoTracking().AsQueryable();
    }

    public async Task<List<WorkTime>> GetWorkTimesWithinMonthByUserIdAsync(ulong userId, int year, int month, CancellationToken cancellationToken)
    {
        return await _context.WorkTimes
            .Where(x => x.UserId == userId && ((
                        x.StartDateTime.Month == month &&
                        x.StartDateTime.Year == year) || (x.StartDateTime.Month == DateTime.Today.Month && x.StartDateTime.Year == DateTime.Today.Year)))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsWorkTimeAlreadyCreatedTodayAsync(ulong userId, CancellationToken cancellationToken)
    {
        return await _context.WorkTimes.AnyAsync(workTime => workTime.UserId == userId && workTime.StartDateTime == DateTime.Today, cancellationToken);
    }

    public async Task<WorkTime?> GetWorkTimeTodayAsync(ulong userId, CancellationToken cancellationToken)
    {
        return await _context.WorkTimes
            .Where(workTime => workTime.UserId == userId && workTime.StartDateTime.Date == DateTime.Today)
            .OrderByDescending(workTime => workTime.StartDateTime)
            .FirstOrDefaultAsync(cancellationToken);
    }
}