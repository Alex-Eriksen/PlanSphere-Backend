using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class RoleRepository(
    IPlanSphereDatabaseContext dbContext,
    ILogger<RoleRepository> logger
) : IRoleRepository
{
    private readonly IPlanSphereDatabaseContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly ILogger<RoleRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<Role> CreateAsync(Role request, CancellationToken cancellationToken)
    {
        _dbContext.Roles.Add(request);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return request;
    }

    public async Task<Role> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        var role = await _dbContext.Roles
            .Include(x => x.OrganisationRole).ThenInclude(x => x.Organisation)
            .Include(x => x.CompanyRole).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.DepartmentRole).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.TeamRole).ThenInclude(x => x.Team).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.CreatedByUser)
            .Include(x => x.OrganisationRoleRights).ThenInclude(x => x.Right)
            .Include(x => x.CompanyRoleRights).ThenInclude(x => x.Right)
            .Include(x => x.DepartmentRoleRights).ThenInclude(x => x.Right)
            .Include(x => x.TeamRoleRights).ThenInclude(x => x.Right)
            .AsSplitQuery()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (role == null)
        {
            _logger.LogInformation("Couldn't find role with id: [{roleId}]", id);
            throw new KeyNotFoundException($"Couldn't find role with id: [{id}]");
        }

        return role;
    }

    public async Task<Role> UpdateAsync(ulong id, Role request, CancellationToken cancellationToken)
    {
        _dbContext.Roles.Update(request);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return request;
    }

    public async Task<Role> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        var role = await _dbContext.Roles.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (role == null)
        {
            _logger.LogInformation("Couldn't find role with id: [{roleId}]", id);
            throw new KeyNotFoundException($"Couldn't find role with id: [{id}]");
        }

        dbContext.Roles.Remove(role);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return role;
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