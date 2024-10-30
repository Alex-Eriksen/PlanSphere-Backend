using MediatR;

namespace PlanSphere.Core.Features.Users.Commands.LoginUser;

public record LoginUserCommand(string Email, string Password) : IRequest<string>;