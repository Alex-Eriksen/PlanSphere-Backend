using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class OrganisationSettings
{
    public ulong OrganisationId { get; set; }
    public virtual Organisation Organisation { get; set; }
    
    public ulong DefaultRoleId { get; set; }
    public virtual Role DefaultRole { get; set; }
    
    public ulong DefaultWorkScheduleId { get; set; }
    public virtual WorkSchedule DefaultWorkSchedule { get; set; }
}