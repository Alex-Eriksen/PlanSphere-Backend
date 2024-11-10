using PlanSphere.Core.Features.Addresses.Requests;

namespace PlanSphere.Core.Features.Teams.Request;

public class TeamViewRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Identifier { get; set; }
    public AddressRequest Address { get; set; }
    public TeamSettingsRequest? Settings { get; set; }
}