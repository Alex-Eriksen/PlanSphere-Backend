using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Features.Users.Requests;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Users.Commands.CreateUser;

public record CreateUserCommand(UserRequest Request, bool WithConfirmationEmail = false) : IRequest, ISourceLevelRequest
{
    public ulong UserId { get; set; }
    public ulong OrganisationId { get; set; }
    public SourceLevel SourceLevel { get; set; }
    public ulong SourceLevelId { get; set; }
}