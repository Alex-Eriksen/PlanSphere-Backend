using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Roles.Commands.ToggleInheritance;

public record ToggleRoleInheritanceCommand(ulong RoleId) : IRequest, ISourceLevelRequest
{
    public SourceLevel SourceLevel { get; set; }
    public ulong SourceLevelId { get; set; }
}
