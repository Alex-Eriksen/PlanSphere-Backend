using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class JobTitleRepository(IPlanSphereDatabaseContext context, ILogger<JobTitleRepository> logger) : IJobTitleRepository
{
private readonly IPlanSphereDatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));
private readonly ILogger<JobTitleRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<JobTitle> CreateAsync(JobTitle request, CancellationToken cancellationToken)
    {
        _context.JobTitles.Add(request);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<JobTitle> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        var jobTitle = await _context.JobTitles.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (jobTitle == null)
        {
            _logger.LogInformation("Could not find job title with id: [{jobTitleId}]. Job title doesn't exist!", id);
            throw new KeyNotFoundException($"Could not find job title with id: [{id}]. Job title doesn't exist!");
        }

        return jobTitle;
    }

    public async Task<JobTitle> UpdateAsync(ulong id, JobTitle request, CancellationToken cancellationToken)
    {
        _context.JobTitles.Update(request);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<JobTitle> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        var jobTitle = await _context.JobTitles.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (jobTitle == null)
        {
            _logger.LogInformation("Could not find job title with id: [{jobTitleId}]. Job title doesn't exist!", id);
            throw new KeyNotFoundException($"Could not find job title with id: [{id}]. Job title doesn't exist!");
        }

        _context.JobTitles.Remove(jobTitle);
        await _context.SaveChangesAsync(cancellationToken);
        return jobTitle;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<JobTitle> GetQueryable()
    {
        return _context.JobTitles
            .Include(x => x.OrganisationJobTitle)
            .Include(x => x.CompanyJobTitle)
            .Include(x => x.DepartmentJobTitle)
            .Include(x => x.TeamJobTitle)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.CreatedByUser)
            .AsNoTracking()
            .AsQueryable();
    }
}