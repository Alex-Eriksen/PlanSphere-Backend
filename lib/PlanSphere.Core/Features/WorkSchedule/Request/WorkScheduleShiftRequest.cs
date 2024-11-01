using Domain.Entities.EmbeddedEntities;

namespace PlanSphere.Core.Features.WorkSchedule.Request;

public class WorkScheduleShiftRequest
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DayOfWeek Day { get; set; }
    public ShiftLocation? Location { get; set; }
}