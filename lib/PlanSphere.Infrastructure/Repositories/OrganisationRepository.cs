using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class OrganisationRepository(IPlanSphereDatabaseContext context, ILogger<OrganisationRepository> logger) : IOrganisationRepository
{
    private readonly IPlanSphereDatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<OrganisationRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<Organisation> CreateAsync(Organisation request, CancellationToken cancellationToken)
    {
        _context.Organisations.Add(request);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<Organisation> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        var organisation = await _context.Organisations.Include(x => x.Address).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (organisation == null)
        {
            _logger.LogInformation("Could not find Organisation with id: [{organisationId}]. Organisation doesn't exist!", id);
            throw new KeyNotFoundException($"Could not find Organisation with id: [{id}]. Organisation doesn't exist!");
        }

        return organisation;
    }

    public async Task<Organisation> UpdateAsync(ulong id, Organisation request, CancellationToken cancellationToken)
    {
        _context.Organisations.Update(request);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<Organisation> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        var organisation = await  _context.Organisations.SingleOrDefaultAsync(org => org.Id == id, cancellationToken);
        if (organisation == null)
        {
            _logger.LogInformation("Organisation with id: [{id}] does not exist", id);
            throw new KeyNotFoundException($"Organisation with id: [{id}] does not exist");
        }
        
        _context.Organisations.Remove(organisation);
        await _context.SaveChangesAsync(cancellationToken);
        return organisation;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Organisation> GetQueryable()
    {
        return _context.Organisations
            .Include(o => o.Users)
                .ThenInclude(u => u.Roles)
                .ThenInclude(r => r.Role)
                .ThenInclude(x => x.OrganisationRole)
            .Include(o => o.Users)
                .ThenInclude(u => u.Roles)
                .ThenInclude(r => r.Role)
                .ThenInclude(x => x.CompanyRole)
            .Include(o => o.Users)
                .ThenInclude(u => u.Roles)
                .ThenInclude(r => r.Role)
                .ThenInclude(x => x.DepartmentRole)
            .Include(o => o.Users)
                .ThenInclude(u => u.Roles)
                .ThenInclude(r => r.Role)
                .ThenInclude(x => x.TeamRole).AsQueryable();
    }
}