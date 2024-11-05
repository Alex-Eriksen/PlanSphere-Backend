using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums;

namespace PlanSphere.Core.Features.Roles.DTOs;

public class RoleListItemDTO : BaseDTO
{
    public string Name { get; set; }
    public int Rights { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public bool IsInheritanceActive { get; set; }
    public SourceLevel SourceLevel { get; set; }
}