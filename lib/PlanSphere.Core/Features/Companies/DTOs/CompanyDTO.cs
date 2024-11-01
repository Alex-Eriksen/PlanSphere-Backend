using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanSphere.Core.Features.Address.DTOs;
using PlanSphere.Core.Features.Address.Requests;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Companies.DTOs;

public class CompanyDTO : IContactable
{
    public string Name { get; set; }
    public string CVR { get; set; }
    public string? ContactName { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhoneNumber { get; set; }
    public AddressDTO Address { get; set; }
    public string? CareOf { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}