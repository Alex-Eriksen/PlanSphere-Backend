using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class WorkSchedule : BaseEntity
{
    public ulong? ParentId { get; set; }
    public virtual WorkSchedule? Parent { get; set; }
    public virtual List<WorkSchedule>? Children { get; set; }
    
    public bool IsDefaultWorkSchedule { get; set; }
    
    public virtual OrganisationSettings? OrganisationSettings { get; set; }
    public virtual CompanySettings? CompanySettings { get; set; }
    public virtual DepartmentSettings? DepartmentSettings { get; set; }
    public virtual TeamSettings? TeamSettings { get; set; }
    public virtual UserSettings? UserSettings { get; set; }

    public List<WorkScheduleShift> WorkScheduleShifts { get; set; } = new List<WorkScheduleShift>();
}