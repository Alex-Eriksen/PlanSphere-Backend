using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Abstract;

namespace PlanSphere.Core.Features.WorkSchedules.Request;

public class WorkScheduleShiftRequest : BaseDTO
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DayOfWeek Day { get; set; }
    public ShiftLocation? Location { get; set; }
}