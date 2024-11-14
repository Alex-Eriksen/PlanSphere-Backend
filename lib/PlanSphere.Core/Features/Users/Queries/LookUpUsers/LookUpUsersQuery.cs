using MediatR;
using PlanSphere.Core.Features.Users.DTOs;

namespace PlanSphere.Core.Features.Users.Queries.LookUpUsers;

public record LookUpUsersQuery(ulong OrganisationId) : IRequest<List<UserLookUpDTO>>;
