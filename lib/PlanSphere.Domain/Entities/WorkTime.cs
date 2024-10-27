using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class WorkTime : BaseEntity, IAuditableEntity
{
    public ulong UserId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public WorkTimeType WorkTimeType { get; set; }
    
    public ShiftLocation? Location { get; set; }
    public DateTime? ApprovalDateTime { get; set; }
    public ulong? ApprovedBy { get; set; }
    public User? ApprovedByUser { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public ulong? CreatedBy { get; set; }
    public User? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ulong? UpdatedBy { get; set; }
    public User? UpdatedByUser { get; set; }
}