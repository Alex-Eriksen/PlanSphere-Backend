using Domain.Entities;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Common.DTOs;
using PlanSphere.Core.Features.Addresses.DTOs;

namespace PlanSphere.Core.Features.Organisations.DTOs;

public class OrganisationDetailDTO : BaseDTO, IAuditableEntityDTO
{
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
    public AddressDTO Address { get; set; }
    public OrganisationSettingsDTO Settings { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}