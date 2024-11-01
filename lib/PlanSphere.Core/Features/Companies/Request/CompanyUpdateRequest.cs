using PlanSphere.Core.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanSphere.Core.Features.Address.Requests;

namespace PlanSphere.Core.Features.Companies.Request;
public class CompanyUpdateRequest
{
    public string CompanyName { get; set; }
    public string CompanyLogo { get; set; }
    public AddressRequest Address { get; set; }
    //public string StreetName { get; set; }
    //public string HouseNumber { get; set; }
    //public string Floor { get; set; }
    //public string ZipCode { get; set; }
    //public string City { get; set; }
    public string ContactEmail { get; set; }
    public string ContactPhoneNumber { get; set; }
    public string ContactName { get; set; }
}