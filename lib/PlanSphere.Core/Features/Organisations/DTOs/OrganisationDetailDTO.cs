using Domain.Entities;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Features.Addresses.DTOs;

namespace PlanSphere.Core.Features.Organisations.DTOs;

public class OrganisationDetailDTO : BaseDTO
{
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
    public AddressDTO Address { get; set; }
    public DateTime CreatedAt { get; set; }
}