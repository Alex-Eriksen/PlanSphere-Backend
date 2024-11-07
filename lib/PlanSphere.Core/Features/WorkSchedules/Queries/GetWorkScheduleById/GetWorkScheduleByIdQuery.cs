using MediatR;
using PlanSphere.Core.Features.WorkSchedules.DTOs;

namespace PlanSphere.Core.Features.WorkSchedules.Queries.GetWorkScheduleById;

public record GetWorkScheduleByIdQuery(ulong WorkScheduleId) : IRequest<WorkScheduleDTO>;
