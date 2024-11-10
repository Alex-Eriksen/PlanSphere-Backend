using MediatR;
using PlanSphere.Core.Features.WorkSchedules.DTOs;

namespace PlanSphere.Core.Features.WorkSchedules.Queries.LookUpWorkSchedules;

public record LookUpWorkSchedulesQuery(ulong UserId) : IRequest<List<WorkScheduleLookUpDTO>>;
