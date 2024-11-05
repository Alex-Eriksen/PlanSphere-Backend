using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class JobTitle : BaseEntity, IAuditableEntity
{
    public string Name { get; set; }
    
    public virtual OrganisationJobTitle? OrganisationJobTitle { get; set; }
    public virtual CompanyJobTitle? CompanyJobTitle { get; set; }
    public virtual DepartmentJobTitle? DepartmentJobTitle { get; set; }
    public virtual TeamJobTitle? TeamJobTitle { get; set; }

    public virtual List<CompanyBlockedJobTitle> CompanyBlockedJobTitles { get; set; } = new List<CompanyBlockedJobTitle>();
    public virtual List<DepartmentBlockedJobTitle> DepartmentBlockedJobTitles { get; set; } = new List<DepartmentBlockedJobTitle>();
    public virtual List<TeamBlockedJobTitle> TeamBlockedJobTitles { get; set; } = new List<TeamBlockedJobTitle>();
    
    public DateTime CreatedAt { get; set; }
    public ulong? CreatedBy { get; set; }
    public User? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ulong? UpdatedBy { get; set; }
    public User? UpdatedByUser { get; set; }
    
    public SourceLevel SourceLevel
    {
        get
        {
            if (OrganisationJobTitle != null)
            {
                return SourceLevel.Organisation;
            }
            if (CompanyJobTitle != null)
            {
                return SourceLevel.Company;
            }
            if (DepartmentJobTitle != null)
            {
                return SourceLevel.Department;
            }

            if (TeamJobTitle != null)
            {
                return SourceLevel.Team;
            }

            throw new NullReferenceException();
        }
    }
}