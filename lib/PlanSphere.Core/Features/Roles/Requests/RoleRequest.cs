namespace PlanSphere.Core.Features.Roles.Requests;

public class RoleRequest
{
    public string Name { get; set; }

    public List<RoleRightRequest> Rights { get; set; } = new List<RoleRightRequest>();
}