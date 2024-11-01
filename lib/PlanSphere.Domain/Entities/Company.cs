using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class Company : BaseEntity, IAuditableEntity
{
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
    public string? VAT { get; set; } // CVR
    
    public ulong OrganisationId { get; set; }
    public virtual Organisation Organisation { get; set; }
    
    public ulong AddressId { get; set; }
    public virtual Address Address { get; set; }
    
    public ulong SettingsId { get; set; }
    public virtual CompanySettings Settings { get; set; }

    public List<Department> Departments { get; set; } = new List<Department>();
    
    public DateTime CreatedAt { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactName { get; set; }
    public string? ContactPhoneNumber { get; set; }
    public string? CareOf { get; set; }
    public ulong? CreatedBy { get; set; }
    public User? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ulong? UpdatedBy { get; set; }
    public User? UpdatedByUser { get; set; }
}