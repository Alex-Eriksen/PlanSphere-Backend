using Domain.Entities;

namespace PlanSphere.Core.Interfaces.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<List<Right>> GetRightsAsync(CancellationToken cancellationToken);
}