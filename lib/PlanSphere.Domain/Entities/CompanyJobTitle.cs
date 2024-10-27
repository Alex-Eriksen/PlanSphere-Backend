namespace Domain.Entities;

public class CompanyJobTitle
{
    public ulong CompanyId { get; set; }
    public ulong JobTitleId { get; set; }
    public bool IsInheritanceActive { get; set; }
}