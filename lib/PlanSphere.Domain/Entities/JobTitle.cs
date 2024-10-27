using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class JobTitle : BaseEntity, IAuditableEntity
{
    public string Name { get; set; }
    
    public OrganisationJobTitle? OrganisationJobTitle { get; set; }
    public CompanyJobTitle? CompanyJobTitle { get; set; }
    public DepartmentJobTitle? DepartmentJobTitle { get; set; }
    public TeamJobTitle? TeamJobTitle { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public ulong? CreatedBy { get; set; }
    public User? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ulong? UpdatedBy { get; set; }
    public User? UpdatedByUser { get; set; }
}