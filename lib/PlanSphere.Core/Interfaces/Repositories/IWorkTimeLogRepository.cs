using Domain.Entities;

namespace PlanSphere.Core.Interfaces.Repositories;

public interface IWorkTimeLogRepository : IRepository<WorkTimeLog>
{
    Task<WorkTimeLog> GetUncheckedLogAsync(ulong userId, CancellationToken cancellationToken);
}