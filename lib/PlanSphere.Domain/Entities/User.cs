using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class User : BaseEntity, IAuditableEntity
{
    public ulong OrganisationId { get; set; }
    public virtual Organisation Organisation { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => string.Join(" ", FirstName, LastName);
    
    public string AddressId { get; set; }
    public virtual Address Address { get; set; }
    
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly? Birthday { get; set; }
    public string? ProfilePictureUrl { get; set; }
    
    public string AuthId { get; set; }
    
    public ulong SettingsId { get; set; }
    public virtual UserSettings Settings { get; set; }

    public List<UserRole> Roles { get; set; } = new List<UserRole>();
    
    public DateTime CreatedAt { get; set; }
    public ulong? CreatedBy { get; set; }
    public User? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ulong? UpdatedBy { get; set; }
    public User? UpdatedByUser { get; set; }
}