using MediatR;
using PlanSphere.Core.Features.Teams.DTOs;

namespace PlanSphere.Core.Features.Teams.Queries.LookUpTeams;

public record LookUpTeamsQuery(ulong UserId) : IRequest<List<TeamLookUpDTO>>;
