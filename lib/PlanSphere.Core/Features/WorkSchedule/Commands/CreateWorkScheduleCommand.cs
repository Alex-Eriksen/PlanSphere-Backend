using MediatR;
using PlanSphere.Core.Features.WorkSchedule.Request;

namespace PlanSphere.Core.Features.WorkSchedule.Commands.CreateWorkSchedule;

public record CreateWorkScheduleCommand (WorkScheduleRequest Request) : IRequest
{
    public ulong OrganisationId { get; set; }

}