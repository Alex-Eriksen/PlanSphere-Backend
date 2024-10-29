namespace PlanSphere.Core.Features.Organisations.Requests;

public class CompanyRequest
{
    public string Name { get; set; }
    public string CVR { get; set; }
    public Adress Adress { get; set; }
    public string CareOf { get; set; }
    public string ContactMail { get; set; }
    public string ContactPhoneNumber { get; set; }
    public string ContactName { get; set; }
}