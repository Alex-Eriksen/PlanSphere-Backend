using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Enums;
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
        var jobTitle = await _context.JobTitles
            .Include(x => x.OrganisationJobTitle)
            .Include(x => x.CompanyJobTitle)
            .Include(x => x.DepartmentJobTitle)
            .Include(x => x.TeamJobTitle)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
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
            .Include(x => x.CompanyBlockedJobTitles)
            .Include(x => x.DepartmentBlockedJobTitles)
            .Include(x => x.TeamBlockedJobTitles)
            .Include(x => x.OrganisationJobTitle).ThenInclude(x => x.Organisation)
            .Include(x => x.CompanyJobTitle).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.DepartmentJobTitle).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.TeamJobTitle).ThenInclude(x => x.Team).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.CreatedByUser)
            .AsNoTracking()
            .AsQueryable();
    }

    public async Task<bool> ToggleInheritanceAsync(ulong jobTitleId, CancellationToken cancellationToken)
    {
        var jobTitle = await GetByIdAsync(jobTitleId, cancellationToken);
        if (jobTitle.OrganisationJobTitle != null)
            jobTitle.OrganisationJobTitle.IsInheritanceActive = !jobTitle.OrganisationJobTitle.IsInheritanceActive;
        if (jobTitle.CompanyJobTitle != null)
            jobTitle.CompanyJobTitle.IsInheritanceActive = !jobTitle.CompanyJobTitle.IsInheritanceActive;
        if (jobTitle.DepartmentJobTitle != null)
            jobTitle.DepartmentJobTitle.IsInheritanceActive = !jobTitle.DepartmentJobTitle.IsInheritanceActive;
        if (jobTitle.TeamJobTitle != null)
            jobTitle.TeamJobTitle.IsInheritanceActive = !jobTitle.TeamJobTitle.IsInheritanceActive;
        await _context.SaveChangesAsync(cancellationToken);
        return GetInheritance(jobTitle);

    }

    public IQueryable<JobTitle> GetCompanyJobTitles(ulong companyId, IQueryable<JobTitle> query)
    {
        return query.Where(x => (x.CompanyJobTitle != null && x.CompanyJobTitle.CompanyId == companyId) ||
                                (x.OrganisationJobTitle != null &&
                                x.OrganisationJobTitle.IsInheritanceActive &&
                                x.CompanyBlockedJobTitles.All(cbj => cbj.CompanyId != companyId)));
    }
    
    public IQueryable<JobTitle> GetDepartmentJobTitles(ulong departmentId, IQueryable<JobTitle> query)
    {
        var companyId = _context.Departments
            .Where(d => d.Id == departmentId)
            .Select(d => d.CompanyId)
            .FirstOrDefault();

        return query.Where(x =>
            (x.OrganisationJobTitle != null &&
             x.OrganisationJobTitle.IsInheritanceActive &&
             x.CompanyBlockedJobTitles.All(cbj => cbj.JobTitleId != x.OrganisationJobTitle.JobTitleId)) ||
            
            (x.CompanyJobTitle != null &&
             x.CompanyJobTitle.IsInheritanceActive &&
             x.CompanyBlockedJobTitles.All(cbj => cbj.CompanyId != companyId) &&
             x.DepartmentBlockedJobTitles.All(dbj => dbj.DepartmentId != departmentId)) ||

            (x.DepartmentJobTitle != null &&
             x.DepartmentJobTitle.DepartmentId == departmentId &&
             x.CompanyBlockedJobTitles.All(cbj => cbj.CompanyId != companyId) &&
             x.DepartmentBlockedJobTitles.All(dbj => dbj.DepartmentId != departmentId)));
    }


    public IQueryable<JobTitle> GetTeamJobTitles(ulong teamId, IQueryable<JobTitle> query)
    {
        var team = _context.Teams
            .Where(t => t.Id == teamId)
            .Select(t => new { t.DepartmentId, t.Department.CompanyId })
            .FirstOrDefault();

        return query.Where(x =>
            (x.OrganisationJobTitle != null &&
             x.OrganisationJobTitle.IsInheritanceActive &&
             x.CompanyBlockedJobTitles.All(cbj => cbj.JobTitleId != x.OrganisationJobTitle.JobTitleId) &&
             x.DepartmentBlockedJobTitles.All(cbj => cbj.JobTitleId != x.OrganisationJobTitle.JobTitleId) &&
             x.TeamBlockedJobTitles.All(cbj => cbj.JobTitleId != x.OrganisationJobTitle.JobTitleId)) ||

            (x.CompanyJobTitle != null &&
             x.CompanyJobTitle.IsInheritanceActive &&
             x.CompanyBlockedJobTitles.All(cbj => cbj.CompanyId != team.CompanyId) &&
             x.DepartmentBlockedJobTitles.All(dbj => dbj.DepartmentId != team.DepartmentId) &&
             x.TeamBlockedJobTitles.All(tbj => tbj.TeamId != teamId)) ||

            (x.DepartmentJobTitle != null &&
             x.DepartmentJobTitle.IsInheritanceActive &&
             x.CompanyBlockedJobTitles.All(cbj => cbj.CompanyId != team.CompanyId) &&
             x.DepartmentBlockedJobTitles.All(dbj => dbj.DepartmentId != team.DepartmentId) &&
             x.TeamBlockedJobTitles.All(tbj => tbj.TeamId != teamId)) ||

            (x.TeamJobTitle != null &&
             x.TeamJobTitle.TeamId == teamId &&
             x.CompanyBlockedJobTitles.All(cbj => cbj.CompanyId != team.CompanyId) &&
             x.DepartmentBlockedJobTitles.All(dbj => dbj.DepartmentId != team.DepartmentId) &&
             x.TeamBlockedJobTitles.All(tbj => tbj.TeamId != teamId)));

    }

    private bool GetInheritance(JobTitle jobTitle)
    {
        if (jobTitle.OrganisationJobTitle != null)
            return jobTitle.OrganisationJobTitle.IsInheritanceActive;
        if (jobTitle.CompanyJobTitle != null)
            return jobTitle.CompanyJobTitle.IsInheritanceActive;
        if (jobTitle.DepartmentJobTitle != null)
            return jobTitle.DepartmentJobTitle.IsInheritanceActive;
        return jobTitle.TeamJobTitle.IsInheritanceActive;
    }
}