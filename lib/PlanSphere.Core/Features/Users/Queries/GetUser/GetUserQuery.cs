using MediatR;
using PlanSphere.Core.Features.Users.DTOs;

namespace PlanSphere.Core.Features.Users.Queries.GetUser;

public record GetUserQuery(ulong UserId) : IRequest<UserListDTO>;
