using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(ulong UserId) : IRequest, ISourceLevelRequest
{
    public SourceLevel SourceLevel { get; set; }
    public ulong SourceLevelId { get; set; }
}
