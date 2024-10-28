namespace Domain.Entities;

public class DepartmentJobTitle
{
    public ulong DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    
    public ulong JobTitleId { get; set; }
    public virtual JobTitle JobTitle { get; set; }
    
    public bool IsInheritanceActive { get; set; }
}