using PlanSphere.Core.Features.Addresses.Requests;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Companies.Request;

public class CompanyRequest : IContactable
{
    public string Name { get; set; }
    public string CVR { get; set; }
    public AddressRequest Address { get; set; }
    public bool InheritAddress { get; set; }
    public string? CareOf { get; set; }
    public string? ContactName { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhoneNumber { get; set; }

}
