using PlanSphere.Core.Abstract;
using PlanSphere.Core.Features.Addresses.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Companies.DTOs;

public class CompanyDTO : BaseDTO, IContactable
{
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public string CVR { get; set; }
    public string? ContactName { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhoneNumber { get; set; }
    public AddressDTO Address { get; set; }
    public bool InheritAddress { get; set; }
    public CompanySettingsDTO Settings { get; set; }
    public string? CareOf { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}