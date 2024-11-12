using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class DepartmentRepository(IPlanSphereDatabaseContext context, ILogger<DepartmentRepository> logger) : IDepartmentRepository
{
    private readonly IPlanSphereDatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<DepartmentRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    public async Task<Department> CreateAsync(Department request, CancellationToken cancellationToken)
    {
        _context.Departments.Add(request);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<Department> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        var department = await _context.Departments
            .Include(x => x.Address)
            .Include(x => x.Company).ThenInclude(x => x.Settings)
            .Include(x => x.Settings).ThenInclude(x => x.DefaultWorkSchedule).ThenInclude(x => x.Parent).ThenInclude(x => x.Parent).ThenInclude(x => x.WorkScheduleShifts)
            .Include(x => x.Settings).ThenInclude(x => x.DefaultWorkSchedule).ThenInclude(x => x.Parent).ThenInclude(x => x.WorkScheduleShifts)
            .Include(x => x.Settings).ThenInclude(x => x.DefaultWorkSchedule).ThenInclude(x => x.WorkScheduleShifts)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (department == null)
        {
            _logger.LogInformation("Could not find department with the id: [{departmentId}]. Department doesn't exist!", id);
            throw new KeyNotFoundException($"Could not find deparment with id: [{id}]. Department doesn't exist!");
        }

        return department;
    }

    public async Task<Department> UpdateAsync(ulong id, Department request, CancellationToken cancellationToken)
    {
        _context.Departments.Update(request);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<Department> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        var department = await _context.Departments.SingleOrDefaultAsync(d => d.Id == id, cancellationToken);
        if (department == null)
        {
            _logger.LogInformation("Could not find department with the id: [{departmentId}]. Department doesn't exist!", id);
            throw new KeyNotFoundException($"Could not find deparment with id: [{id}]. Department doesn't exist!");
        }

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync(cancellationToken);
        return department;
    }
    public IQueryable<Department> GetQueryable()
    {
        return _context.Departments
            .Include(x => x.Address).ThenInclude(x => x.ZipCode).ThenInclude(x => x.Country)
            .AsNoTracking()
            .AsQueryable();
    }
    
    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

}