namespace Domain.Entities;

public class DepartmentBlockedJobTitle
{
    public ulong DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    
    public ulong JobTitleId { get; set; }
    public virtual JobTitle JobTitle { get; set; }
}