namespace Domain.Entities;

public class DepartmentSettings
{
    public ulong DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    
    public ulong DefaultRoleId { get; set; }
    public ulong DefaultWorkScheduleId { get; set; }
    public bool InheritDefaultWorkSchedule { get; set; }
}