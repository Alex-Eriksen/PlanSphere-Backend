namespace Domain.Entities;

public class OrganisationRole
{
    public ulong OrganisationId { get; set; }
    public ulong RoleId { get; set; }
    public bool IsInheritanceActive { get; set; }
}