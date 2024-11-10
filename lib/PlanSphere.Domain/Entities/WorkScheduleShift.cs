using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class WorkScheduleShift : BaseEntity
{
    public ulong WorkScheduleId { get; set; }
    public WorkSchedule WorkSchedule { get; set; }
    
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DayOfWeek Day { get; set; }
    public ShiftLocation? Location { get; set; }
}