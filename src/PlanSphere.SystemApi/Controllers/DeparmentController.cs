using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Departments.Commands.CreateDepartment;
using PlanSphere.Core.Features.Departments.Commands.DeleteDepartment;
using PlanSphere.Core.Features.Departments.Commands.PatchDepartments;
using PlanSphere.Core.Features.Departments.Commands.UpdateDepartments;
using PlanSphere.Core.Features.Departments.Queries.GetDepartment;
using PlanSphere.Core.Features.Departments.Queries.ListDepartments;
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
        await _mediator.Send(command);
        return Created();
    }

    [HttpDelete("{sourceLevelId}/{departmentId}", Name = nameof(DeleteDepartmentAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Company])]
    public async Task<IActionResult> DeleteDepartmentAsync([FromRoute] ulong sourceLevelId, [FromRoute] ulong departmentId)
    {
        var command = new DeleteDepartmentCommand(departmentId);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{sourceLevelId}", Name = nameof(PatchDepartmentAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Department])]
    public async Task<IActionResult> PatchDepartmentAsync([FromRoute] ulong sourceLevelId, [FromBody] JsonPatchDocument<DepartmentUpdateRequest> patchRequest)
    {
        var command = new PatchDepartmentCommand(patchRequest);
        command.DepartmentId = sourceLevelId;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{sourceLevelId}", Name = nameof(UpdateDepartmentAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Department])]
    public async Task<IActionResult> UpdateDepartmentAsync([FromRoute] ulong sourceLevelId, [FromBody] DepartmentUpdateRequest request)
    {
        var command = new UpdateDepartmentCommand(request)
        {
            DepartmentId = sourceLevelId
        };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{sourceLevelId}", Name = nameof(ListDepartmentsAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View, SourceLevel.Company])]
    public async Task<IActionResult> ListDepartmentsAsync([FromRoute] ulong sourceLevelId, [FromQuery] ListDepartmentQuery query)
    {
        query.CompanyId = sourceLevelId;
        var respone = await _mediator.Send(query);
        return Ok(respone);
    }
    
    //[HttpGet("{sourceLevelId}/{userId}", Name = nameof(ListUserDepartmentsAsync))]
    //[TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View, SourceLevel.Company])]
    //public async Task<IActionResult> ListUserDepartmentsAsync([])



}