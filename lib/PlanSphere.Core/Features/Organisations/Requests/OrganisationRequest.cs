using PlanSphere.Core.Features.Companies.Requests;
using PlanSphere.Core.Features.JobTitle.Requests;
using PlanSphere.Core.Features.Roles.Requests;

namespace PlanSphere.Core.Features.Organisations.Requests;

public class OrganisationRequest
{
    public string Name { get; set; }
    public List<ulong> MemberIds { get; set; }
    public List<CompanyRequest> CompanyRequests { get; set; }
    public List<JobTitleRequest> JobTitleRequests { get; set; }
    public List<RoleRequest> RoleRequests { get; set; }
}