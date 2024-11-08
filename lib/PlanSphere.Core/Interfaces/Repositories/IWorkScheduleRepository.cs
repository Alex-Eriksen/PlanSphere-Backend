using Domain.Entities;

namespace PlanSphere.Core.Interfaces.Repositories;

public interface IWorkScheduleRepository : IRepository<WorkSchedule>
{
    IQueryable<WorkSchedule> GetWorkScheduleWithSources();
}