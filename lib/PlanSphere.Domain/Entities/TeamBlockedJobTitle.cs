namespace Domain.Entities;

public class TeamBlockedJobTitle
{
    public ulong TeamId { get; set; }
    public virtual Team Team { get; set; }
    
    public ulong JobTitleId { get; set; }
    public virtual JobTitle JobTitle { get; set; }
}