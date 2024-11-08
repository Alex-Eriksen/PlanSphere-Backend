using PlanSphere.Core.Features.Addresses.Requests;

namespace PlanSphere.Core.Features.Organisations.Requests;

public class OrganisationRequest
{
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
    public AddressRequest Address { get; set; }
    public OrganisationSettingsRequest? Settings { get; set; }
}