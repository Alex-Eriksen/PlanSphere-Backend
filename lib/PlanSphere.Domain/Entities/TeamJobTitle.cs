namespace Domain.Entities;

public class TeamJobTitle
{
    public ulong TeamId { get; set; }
    public ulong JobTitleId { get; set; }
    public bool IsInheritanceActive { get; set; }
}