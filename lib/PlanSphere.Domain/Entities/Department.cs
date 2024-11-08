using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class Department : BaseEntity, IAuditableEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Building { get; set; }
    public string? LogoUrl { get; set; }
    
    public ulong CompanyId { get; set; }
    public virtual Company Company { get; set; }
    
    public ulong AddressId { get; set; }
    public virtual Address Address { get; set; }
    public bool InheritAddress { get; set; }
    
    public ulong SettingsId { get; set; }
    public virtual DepartmentSettings Settings { get; set; }

    public virtual List<Team> Teams { get; set; } = new List<Team>();

    public virtual List<DepartmentRoleRight> DepartmentRoleRights { get; set; } = new List<DepartmentRoleRight>();
    
    public DateTime CreatedAt { get; set; }
    public ulong? CreatedBy { get; set; }
    public virtual User? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ulong? UpdatedBy { get; set; }
    public virtual User? UpdatedByUser { get; set; }
}