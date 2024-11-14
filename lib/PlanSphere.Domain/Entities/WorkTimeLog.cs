using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class WorkTimeLog : BaseEntity, ILoggable
{
    public DateTime LogTime { get; set; }
    
    public ActionType ActionType { get; set; }
    
    public ulong? UserId { get; set; }
    public User? User { get; set; }
    
    public DateTime? OldStartDateTime { get; set; }
    public DateTime StartDateTime { get; set; }
    
    public DateTime? OldEndDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    
    public WorkTimeType? OldWorkTimeType { get; set; }
    public WorkTimeType WorkTimeType { get; set; }
    
    public ShiftLocation? OldLocation { get; set; }
    public ShiftLocation Location { get; set; }
    
    public ulong? LoggedBy { get; set; }
    public User? LoggedByUser { get; set; }
}