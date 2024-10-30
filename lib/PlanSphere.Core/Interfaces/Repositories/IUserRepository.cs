using Domain.Entities;

namespace PlanSphere.Core.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByIdentityId(string identityId, CancellationToken cancellationToken);
    Task<bool> IsUserRegisteredAsync(string email, CancellationToken cancellationToken);
}