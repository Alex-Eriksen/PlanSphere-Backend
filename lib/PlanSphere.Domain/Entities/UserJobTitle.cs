namespace Domain.Entities;

public class UserJobTitle
{
    public ulong UserId { get; set; }
    public virtual User User { get; set; }
    
    public ulong JobTitleId { get; set; }
    public virtual JobTitle JobTitle { get; set; }
}