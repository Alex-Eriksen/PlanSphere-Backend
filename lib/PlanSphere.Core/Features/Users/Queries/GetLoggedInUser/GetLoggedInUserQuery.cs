using MediatR;
using PlanSphere.Core.Features.Users.DTOs;

namespace PlanSphere.Core.Features.Users.Queries.GetLoggedInUser;

public record GetLoggedInUserQuery(string RefreshToken, ulong ClaimedUserId) : IRequest<LoggedInUserDTO>;
