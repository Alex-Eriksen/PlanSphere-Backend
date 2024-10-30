using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace PlanSphere.Infrastructure.Repositories;

public class UserRepository(IPlanSphereDatabaseContext dbContext, ILogger<UserRepository> logger) : IUserRepository
{
    private readonly IPlanSphereDatabaseContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly ILogger<UserRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<User> CreateAsync(User request, CancellationToken cancellationToken)
    {
        _dbContext.Users.Add(request);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<User> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(x => x.Settings)
            .Include(x => x.Address)
            .SingleOrDefaultAsync(user => user.Id == id, cancellationToken);
        
        if (user == null)
        {
            _logger.LogInformation("Could not find user with id: [{userId}]. User doesn't exist!", id);
            throw new KeyNotFoundException($"Could not find user with id: [{id}]. User doesn't exist!");
        }

        return user;
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

    public async Task<User> GetByIdentityId(string identityId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(x => x.Settings)
            .Include(x => x.Address)
            .SingleOrDefaultAsync(user => user.IdentityUserId == identityId, cancellationToken);
        
        if (user == null)
        {
            _logger.LogInformation("Could not find user with identity id: [{identityId}]. User doesn't exist!", identityId);
            throw new KeyNotFoundException($"Could not find user with identity id: [{identityId}]. User doesn't exist!");
        }

        return user;
    }

    public async Task<bool> IsUserRegisteredAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email == email, cancellationToken);
    }
}