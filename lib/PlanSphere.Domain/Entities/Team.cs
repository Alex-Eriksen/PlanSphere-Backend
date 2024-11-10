using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class Team : BaseEntity, IAuditableEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Identifier { get; set; }
    
    public ulong DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    
    public ulong AddressId { get; set; }
    public virtual Address Address { get; set; }
    public bool InheritAddress { get; set; }
    
    public ulong SettingsId { get; set; }
    public virtual TeamSettings Settings { get; set; }
    public DateTime CreatedAt { get; set; }
    public ulong? CreatedBy { get; set; }
    public User? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ulong? UpdatedBy { get; set; }
    public User? UpdatedByUser { get; set; }
}