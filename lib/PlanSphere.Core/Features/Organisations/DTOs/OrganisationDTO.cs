using Domain.Entities;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Features.JobTitles.DTOs;

namespace PlanSphere.Core.Features.Organisations.DTOs;

public class OrganisationDTO : BaseDTO
{
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
    public List<JobTitleDTO> Jobtitles {get; set; }
    public int OrganisationMembers { get; set; }
    public int CompanyMembers { get; set; }
    public int DepartmentMembers { get; set; }
    public int TeamsMembers { get; set; }
    public int Users { get; set; }
    public int Roles { get; set; }
    
    // public AddressDTO Address { get; set; }
    // public OrganisationSettingsDTO OrganisationSettings { get; set; }
    // public List<CompaniesDTO> Companies { get; set; }
    // public DateTime CreatedAt { get; set; }
    // public string CreatedBy { get; set; }
    // public DateTime? UpdatedAt { get; set; }
    // public ulong? UpdatedBy { get; set; }
    // public User? UpdatedByUser { get; set; }
}