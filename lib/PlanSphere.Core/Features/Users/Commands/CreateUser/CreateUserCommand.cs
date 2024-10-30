using MediatR;
using PlanSphere.Core.Features.Users.Requests;

namespace PlanSphere.Core.Features.Users.Commands.CreateUser;

public record CreateUserCommand(UserRequest Request, bool WithConfirmationEmail = false) : IRequest
{
    public ulong UserId { get; set; }
    public ulong OrganisationId { get; set; }
}