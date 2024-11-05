using PlanSphere.Core.Features.Address.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Companies.DTOs;

public class CompanyDTO : IContactable
{
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public string CVR { get; set; }
    public string? ContactName { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhoneNumber { get; set; }
    public AddressDTO Address { get; set; }
    public string? CareOf { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}