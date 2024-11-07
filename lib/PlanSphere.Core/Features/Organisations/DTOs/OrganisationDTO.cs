using Domain.Entities;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Features.JobTitles.DTOs;

namespace PlanSphere.Core.Features.Organisations.DTOs;

public class OrganisationDTO : BaseDTO
{
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
    public List<JobTitleDTO> JobTitles {get; set; }
    public int OrganisationMembers { get; set; }
    public int CompanyMembers { get; set; }
    public int DepartmentMembers { get; set; }
    public int TeamsMembers { get; set; }
}