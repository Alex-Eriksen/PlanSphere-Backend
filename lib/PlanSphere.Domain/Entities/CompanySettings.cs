namespace Domain.Entities;

public class CompanySettings
{
    public ulong CompanyId { get; set; }
    public virtual Company Company { get; set; }
    
    public ulong DefaultRoleId { get; set; }
    public virtual Role DefaultRole { get; set; }
    
    public ulong DefaultWorkScheduleId { get; set; }
    public virtual WorkSchedule DefaultWorkSchedule { get; set; }
    
    public bool InheritDefaultWorkSchedule { get; set; }
}