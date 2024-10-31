using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; }
    
    public ulong UserId { get; set; }
    public virtual User User { get; set; }
    
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    
    public DateTime CreatedAt { get; set; }
    public string CreatedByIp { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? RevokedByIp { get; set; }
    public string? ReplacedByToken { get; set; }
    public bool IsActive => RevokedAt == null && !IsExpired;
}