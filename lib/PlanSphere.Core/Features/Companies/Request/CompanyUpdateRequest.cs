using PlanSphere.Core.Features.Addresses.Requests;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Companies.Request;
public class CompanyUpdateRequest : IContactable
{
    public string CompanyName { get; set; }
    public string CVR { get; set; }
    public string CompanyLogo { get; set; }
    public AddressRequest Address { get; set; }
    public string? ContactName { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhoneNumber { get; set; }
}