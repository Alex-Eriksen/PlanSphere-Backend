using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.WorkSchedule.Commands.CreateWorkSchedule;
using PlanSphere.Core.Features.WorkSchedule.Request;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WorkScheduleController(IMediator mediator) : ApiControllerBase(mediator)
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        [HttpPost("{organisationId}", Name = nameof(CreateWorkScheduleAsync))]
        public async Task<IActionResult> CreateWorkScheduleAsync([FromRoute] ulong organisationId, [FromBody] CreateWorkScheduleCommand command)
        {
            command.OrganisationId = organisationId;
            await _mediator.Send(command);
            return NoContent();
        }
    }
    
    
}