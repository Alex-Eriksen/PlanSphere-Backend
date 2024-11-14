using Domain.Entities;

namespace PlanSphere.Core.Interfaces.Repositories;

public interface IWorkTimeRepository : IRepository<WorkTime>
{
    Task<List<WorkTime>> GetWorkTimesWithinMonthByUserIdAsync(ulong userId, int year, int month, CancellationToken cancellationToken);
    Task<bool> IsWorkTimeAlreadyCreatedTodayAsync(ulong userId, CancellationToken cancellationToken);
    Task<WorkTime?> GetWorkTimeTodayAsync(ulong userId, CancellationToken cancellationToken);
}