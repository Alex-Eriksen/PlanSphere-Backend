using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class OrganisationRepository(
    IPlanSphereDatabaseContext dbContext
) : IOrganisationRepository
{
    private readonly IPlanSphereDatabaseContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    
    public Task<Organisation> CreateAsync(Organisation request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Organisation> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Organisation> UpdateAsync(ulong id, Organisation request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Organisation> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Organisation> GetQueryable()
    {
        return _dbContext.Organisations.AsNoTracking().AsQueryable();
    }
}