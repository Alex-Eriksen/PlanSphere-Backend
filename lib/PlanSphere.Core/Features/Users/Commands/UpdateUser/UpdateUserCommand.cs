using MediatR;
using PlanSphere.Core.Features.Users.Requests;

namespace PlanSphere.Core.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(ulong Id, UserRequest Request) : IRequest;
