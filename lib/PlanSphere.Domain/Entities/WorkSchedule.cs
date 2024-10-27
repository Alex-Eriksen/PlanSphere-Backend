using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class WorkSchedule : BaseEntity
{
    public ulong? ParentId { get; set; }
    public bool IsDefaultWorkSchedule { get; set; }

    public List<WorkScheduleShift> WorkScheduleShifts { get; set; } = new List<WorkScheduleShift>();
}