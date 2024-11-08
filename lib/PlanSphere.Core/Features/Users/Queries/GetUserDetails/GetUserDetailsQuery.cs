using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Features.Users.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Users.Queries.GetUserDetails;

public record GetUserDetailsQuery(ulong UserId) : IRequest<UserDTO>;
