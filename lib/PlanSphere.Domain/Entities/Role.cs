using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class Role : BaseEntity, IAuditableEntity
{
    public string Name { get; set; }
    
    public virtual OrganisationRole? OrganisationRole { get; set; }
    public virtual CompanyRole? CompanyRole { get; set; }
    public virtual DepartmentRole? DepartmentRole { get; set; }
    public virtual TeamRole? TeamRole { get; set; }

    public virtual List<OrganisationRoleRight> OrganisationRoleRights { get; set; } = new List<OrganisationRoleRight>();
    public virtual List<CompanyRoleRight> CompanyRoleRights { get; set; } = new List<CompanyRoleRight>();
    public virtual List<DepartmentRoleRight> DepartmentRoleRights { get; set; } = new List<DepartmentRoleRight>();
    public virtual List<TeamRoleRight> TeamRoleRights { get; set; } = new List<TeamRoleRight>();

    public virtual List<CompanyBlockedRole> BlockedCompanies { get; set; } = new List<CompanyBlockedRole>();
    public virtual List<DepartmentBlockedRole> BlockedDepartments { get; set; } = new List<DepartmentBlockedRole>();

    public DateTime CreatedAt { get; set; }
    public ulong? CreatedBy { get; set; }
    public virtual User? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ulong? UpdatedBy { get; set; }
    public virtual User? UpdatedByUser { get; set; }
    
    public SourceLevel SourceLevel
    {
        get
        {
            if (OrganisationRole != null)
            {
                return SourceLevel.Organisation;
            }

            if (CompanyRole != null)
            {
                return SourceLevel.Company;
            }

            if (DepartmentRole != null)
            {
                return SourceLevel.Department;
            }

            if (TeamRole != null)
            {
                return SourceLevel.Team;
            }

            throw new NullReferenceException("Most likely missing an include.");
        }
    }
}