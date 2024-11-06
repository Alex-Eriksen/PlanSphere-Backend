using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Departments.Commands.CreateDepartment;
using PlanSphere.Core.Features.Departments.Queries.GetDepartment;
using PlanSphere.Core.Features.Departments.Request;
using PlanSphere.SystemApi.Action_Filters;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers;

[Authorize]
public class DepartmentController(IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View])]
    [HttpGet("{departmentId}/{sourceLevel}/{sourceLevelId}", Name = nameof(GetDepartmentId))]
    public async Task<IActionResult> GetDepartmentId([FromRoute] ulong departmentId)
    {
        var query = new GetDepartmentQuery(departmentId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("{sourceLevelId}",Name = nameof(CreateDepartmentAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Company])]
    public async Task<IActionResult> CreateDepartmentAsync([FromRoute] ulong sourceLevelId,[FromBody] DepartmentRequest request)
    {
        var command = new CreateDepartmentCommand(request);
        command.CompanyId = sourceLevelId;
        await _mediator.Send(request);
        return Created();
    }

}