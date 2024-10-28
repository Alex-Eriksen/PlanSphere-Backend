namespace Domain.Entities;

public class OrganisationJobTitle
{
    public ulong OrganisationId { get; set; }
    public virtual Organisation Organisation { get; set; }
    
    public ulong JobTitleId { get; set; }
    public virtual JobTitle JobTitle { get; set; }
    
    public bool IsInheritanceActive { get; set; }
}