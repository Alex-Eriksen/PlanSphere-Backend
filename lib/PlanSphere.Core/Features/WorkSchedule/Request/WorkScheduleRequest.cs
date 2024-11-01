using PlanSphere.Core.Enums;

namespace PlanSphere.Core.Features.WorkSchedule.Request;

public class WorkScheduleRequest
{
    public ulong SourceLevelId { get; set; }
    public SourceLevel SourceLevel { get; set; }
    public bool IsDefaultWorkSchedule { get; set; }
    public List<WorkScheduleShiftRequest> WorkScheduleShifts { get; set; } = new List<WorkScheduleShiftRequest>();
    
}