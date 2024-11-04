using Microsoft.AspNetCore.Http;
using PlanSphere.Core.Features.Address.Requests;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Companies.Request;

public class CompanyRequest : IContactable
{
        public string CompanyName { get; set; }
        public IFormFile? Logo { get; set; }
        public string CVR { get; set; }
        public AddressRequest Address { get; set; }
        public string? CareOf { get; set; }
        public string? ContactName { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhoneNumber { get; set; }

}
