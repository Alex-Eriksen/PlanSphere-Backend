using Domain.Entities;
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
        throw new NotImplementedException();
    }
}