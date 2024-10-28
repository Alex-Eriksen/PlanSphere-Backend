using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class Role : BaseEntity, IAuditableEntity
{
    public string Name { get; set; }
    
    public virtual OrganisationRole? OrganisationRole { get; set; }
    public virtual CompanyRole? CompanyRole { get; set; }
    public virtual DepartmentRole? DepartmentRole { get; set; }
    public virtual TeamRole? TeamRole { get; set; }

    public List<OrganisationRoleRight> OrganisationRoleRights { get; set; } = new List<OrganisationRoleRight>();
    public List<CompanyRoleRight> CompanyRoleRights { get; set; } = new List<CompanyRoleRight>();
    public List<DepartmentRoleRight> DepartmentRoleRights { get; set; } = new List<DepartmentRoleRight>();
    public List<TeamRoleRight> TeamRoleRights { get; set; } = new List<TeamRoleRight>();

    public DateTime CreatedAt { get; set; }
    public ulong? CreatedBy { get; set; }
    public virtual User? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ulong? UpdatedBy { get; set; }
    public virtual User? UpdatedByUser { get; set; }
}