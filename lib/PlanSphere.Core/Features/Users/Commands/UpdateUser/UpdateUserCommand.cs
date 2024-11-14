using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Features.Users.Requests;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(ulong Id, UserRequest Request) : IRequest, ISourceLevelRequest
{
    public SourceLevel SourceLevel { get; set; }
    public ulong SourceLevelId { get; set; }
}
