using Domain.Entities;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace PlanSphere.Infrastructure.Repositories;

public class UserRepository(IPlanSphereDatabaseContext dbContext) : IUserRepository
{
    private readonly IPlanSphereDatabaseContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    
    public async Task<User> CreateAsync(User request, CancellationToken cancellationToken)
    {
        request.Password = BC.HashPassword(request.Password);
        
        _dbContext.Users.Add(request);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return request;
    }

    public Task<User> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateAsync(ulong id, User request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<User> GetQueryable()
    {
        throw new NotImplementedException();
    }
}