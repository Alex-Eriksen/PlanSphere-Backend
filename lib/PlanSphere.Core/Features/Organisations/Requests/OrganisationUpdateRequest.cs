using PlanSphere.Core.Features.Addresses.Requests;

namespace PlanSphere.Core.Features.Organisations.Requests;

public class OrganisationUpdateRequest
{
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
    public AddressRequest Address { get; set; }
}