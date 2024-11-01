using PlanSphere.Core.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanSphere.Core.Features.Address.Requests;
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