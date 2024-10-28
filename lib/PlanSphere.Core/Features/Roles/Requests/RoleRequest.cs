using PlanSphere.Core.Enums;

namespace PlanSphere.Core.Features.Roles.Requests;

public class RoleRequest
{
    public string Name { get; set; }

    public List<RoleRightRequest> RoleRightRequests { get; set; } = new List<RoleRightRequest>();
}