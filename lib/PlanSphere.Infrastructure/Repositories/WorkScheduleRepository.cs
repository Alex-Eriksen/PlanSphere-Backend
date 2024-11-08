using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class WorkScheduleRepository(IPlanSphereDatabaseContext dbContext, ILogger<WorkScheduleRepository> logger) : IWorkScheduleRepository
{
    private readonly IPlanSphereDatabaseContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly ILogger<WorkScheduleRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<WorkSchedule> CreateAsync(WorkSchedule request, CancellationToken cancellationToken)
    {
        _dbContext.WorkSchedules.Add(request);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<WorkSchedule> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        var workSchedule = await _dbContext.WorkSchedules
            .Include(x => x.Parent).ThenInclude(x => x.Parent).ThenInclude(x => x.Parent).ThenInclude(x => x.Parent).ThenInclude(x => x.WorkScheduleShifts)
            .Include(x => x.Parent).ThenInclude(x => x.Parent).ThenInclude(x => x.Parent).ThenInclude(x => x.WorkScheduleShifts)
            .Include(x => x.Parent).ThenInclude(x => x.Parent).ThenInclude(x => x.WorkScheduleShifts)
            .Include(x => x.Parent).ThenInclude(x => x.WorkScheduleShifts)
            .Include(x => x.WorkScheduleShifts.OrderBy(x => x.Day))
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (workSchedule == null)
        {
            _logger.LogInformation("Work schedule with id: [{id}] does not exist!", id);
            throw new KeyNotFoundException($"Work schedule with id: [{id}] does not exist!");
        }

        return workSchedule;
    }

    public async Task<WorkSchedule> UpdateAsync(ulong id, WorkSchedule request, CancellationToken cancellationToken)
    {
        _dbContext.WorkSchedules.Update(request);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return request;
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
        return _dbContext.WorkSchedules
            .Include(x => x.OrganisationSettings).ThenInclude(x => x.Organisation)
            .Include(x => x.CompanySettings).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.DepartmentSettings).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.TeamSettings).ThenInclude(x => x.Team).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .AsNoTracking()
            .AsQueryable();
    }
}