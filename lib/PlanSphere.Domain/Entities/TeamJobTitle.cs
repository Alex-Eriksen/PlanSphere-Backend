namespace Domain.Entities;

public class TeamJobTitle
{
    public ulong TeamId { get; set; }
    public virtual Team Team { get; set; }
    
    public ulong JobTitleId { get; set; }
    public virtual JobTitle JobTitle { get; set; }
    
    public bool IsInheritanceActive { get; set; }
}