namespace Domain.Entities;

public class DepartmentJobTitle
{
    public ulong DepartmentId { get; set; }
    public ulong JobTitleId { get; set; }
    public bool IsInheritanceActive { get; set; }
}