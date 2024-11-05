using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.WorkSchedule.Commands.CreateWorkSchedule;
using PlanSphere.Core.Features.WorkSchedule.Request;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers;

    public class WorkScheduleController(IMediator mediator) : ApiControllerBase(mediator)
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        [HttpPost("{organisationId}", Name = nameof(CreateWorkScheduleAsync))]
        public async Task<IActionResult> CreateWorkScheduleAsync([FromRoute] ulong organisationId, SourceLevel sourceLevel, ulong sourceLevelId, [FromBody] CreateWorkScheduleCommand command)
        {
            command.OrganisationId = organisationId;
            command.SourceLevel = sourceLevel;
            command.SourceLevelId = sourceLevelId;
            await _mediator.Send(command);
            return NoContent();
        }
    }
    
    
