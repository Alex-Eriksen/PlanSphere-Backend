using Domain.Entities;

namespace PlanSphere.Core.Interfaces.Repositories;

public interface IJobTitleRepository : IRepository<JobTitle>
{
    Task<bool> ToggleInheritanceAsync(ulong jobTitleId, CancellationToken cancellationToken);
    IQueryable<JobTitle> GetDepartmentJobTitles(ulong departmentId, IQueryable<JobTitle> query);
    IQueryable<JobTitle> GetTeamJobTitles(ulong teamId, IQueryable<JobTitle> query);

}