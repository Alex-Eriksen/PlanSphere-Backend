namespace Domain.Entities;

public class TeamSettings
{
    public ulong TeamId { get; set; }
    public virtual Team Team { get; set; }
    
    public ulong DefaultRoleId { get; set; }
    public ulong DefaultWorkScheduleId { get; set; }
    public bool InheritDefaultWorkSchedule { get; set; }
}