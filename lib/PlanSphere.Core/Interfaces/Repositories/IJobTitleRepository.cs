using Domain.Entities;

namespace PlanSphere.Core.Interfaces.Repositories;

public interface IJobTitleRepository : IRepository<JobTitle>
{
    Task<bool> ToggleInheritanceAsync(ulong jobTitleId, CancellationToken cancellationToken);
}