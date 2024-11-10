using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class Organisation : BaseEntity, IAuditableEntity
{
    public string Name { get; set; }
    
    public string? LogoUrl { get; set; }
    
    public ulong AddressId { get; set; }
    public virtual Address Address { get; set; }
    
    public ulong SettingsId { get; set; }
    public virtual OrganisationSettings Settings { get; set; }

    public List<Company> Companies { get; set; } = new List<Company>();

    public DateTime CreatedAt { get; set; }
    public ulong? CreatedBy { get; set; }
    public User? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ulong? UpdatedBy { get; set; }
    public User? UpdatedByUser { get; set; }
    public virtual List<User> Users { get; set; } = new List<User>();
}