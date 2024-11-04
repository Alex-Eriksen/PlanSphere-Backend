using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class RoleRepository(IPlanSphereDatabaseContext dbContext) : IRoleRepository
{
    private readonly IPlanSphereDatabaseContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    
    public async Task<Role> CreateAsync(Role request, CancellationToken cancellationToken)
    {
        _dbContext.Roles.Add(request);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return request;
    }

    public Task<Role> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Role> UpdateAsync(ulong id, Role request, CancellationToken cancellationToken)
    {
        _dbContext.Roles.Update(request);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return request;
    }

    public Task<Role> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Role> GetQueryable()
    {
        return _dbContext.Roles
            .Include(x => x.OrganisationRole).ThenInclude(x => x.Organisation)
            .Include(x => x.CompanyRole).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.DepartmentRole).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.TeamRole).ThenInclude(x => x.Team).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.CreatedByUser)
            .AsNoTracking()
            .AsQueryable();
    }

    public async Task<List<Right>> GetRightsAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Rights.ToListAsync(cancellationToken);
    }
}