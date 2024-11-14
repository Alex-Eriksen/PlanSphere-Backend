using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Features.WorkTimes.Requests;

namespace PlanSphere.Core.Interfaces.Services;

public interface IWorkTimeLogService
{
    Task<WorkTimeLog> CreateLogAsync(WorkTimeRequest workTime, ActionType actionType, CancellationToken cancellationToken);
}