namespace Domain.Entities;

public class OrganisationJobTitle
{
    public ulong OrganisationId { get; set; }
    public ulong JobTitleId { get; set; }
    public bool IsInheritanceActive { get; set; }
}