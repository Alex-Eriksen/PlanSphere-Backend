using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class CountryRepository(IPlanSphereDatabaseContext context) : ICountryRepository
{
    private readonly IPlanSphereDatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));
    public Task<Country> CreateAsync(Country request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Country> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Country> UpdateAsync(ulong id, Country request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Country> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Country> GetQueryable()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Country>> GetCountryLookUps(CancellationToken cancellationToken)
    {
        return await _context.Countries.ToListAsync(cancellationToken);
    }
}