using Domain.Entities.EmbeddedEntities;

namespace PlanSphere.Core.Features.WorkSchedules.Request;

public class WorkScheduleRequest
{
    public ulong SourceLevelId { get; set; }
    public SourceLevel SourceLevel { get; set; }
    public List<WorkScheduleShiftRequest> WorkScheduleShifts { get; set; } = new List<WorkScheduleShiftRequest>();
}