using PlanSphere.Core.Abstract;
using PlanSphere.Core.Features.Addresses.DTOs;

namespace PlanSphere.Core.Features.Teams.DTOs;

public class TeamDTO : BaseDTO
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Identifier { get; set; }
    public AddressDTO Address { get; set; }
    public TeamSettingsDTO SettingsDTO { get; set; }
}