using PlanSphere.Core.Features.Roles.DTOs;

namespace PlanSphere.Core.Features.Users.DTOs;

public class LoggedInUserDTO
{
    public List<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
}