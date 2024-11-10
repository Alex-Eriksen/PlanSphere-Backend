namespace Domain.Entities;

public class CompanyBlockedJobTitle
{
    public ulong CompanyId { get; set; }
    public virtual Company Company { get; set; }
    
    public ulong JobTitleId { get; set; }
    public virtual JobTitle JobTitle { get; set; }
}