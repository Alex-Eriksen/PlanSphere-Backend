using PlanSphere.Core.Features.Address.Requests;

namespace PlanSphere.Core.Features.Companies.Requests;

public class CompanyRequest
{
    public string Name { get; set; }
    public string CVR { get; set; }
    public AddressRequest Adress { get; set; }
    public string CareOf { get; set; }
    public string ContactMail { get; set; }
    public string ContactPhoneNumber { get; set; }
    public string ContactName { get; set; }
}