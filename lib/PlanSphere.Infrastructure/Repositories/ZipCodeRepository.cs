using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class ZipCodeRepository(IPlanSphereDatabaseContext context) : IZipCodeRepository
{
    private readonly IPlanSphereDatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));
    public Task<ZipCode> CreateAsync(ZipCode request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ZipCode> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ZipCode> UpdateAsync(ulong id, ZipCode request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ZipCode> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<ZipCode> GetQueryable()
    {
        throw new NotImplementedException();
    }

    public async Task<List<ZipCode>> GetZipCodeLookUpsAsync(CancellationToken cancellationToken)
    {
        return await _context.ZipCodes.ToListAsync(cancellationToken);
    }
}