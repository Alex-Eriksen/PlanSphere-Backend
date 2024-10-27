namespace Domain.Entities;

public class CompanyRole
{
    public ulong CompanyId { get; set; }
    public ulong RoleId { get; set; }
    public bool IsInheritanceActive { get; set; }
}