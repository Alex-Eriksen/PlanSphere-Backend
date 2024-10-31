using PlanSphere.Core.Enums;

namespace PlanSphere.Core.Features.Roles.DTOs;

public class RoleDTO
{
    public string Name { get; set; }

    public List<RightDTO> Rights { get; set; } = new List<RightDTO>();
}