using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using PlanSphere.Core.Features.Departments.Request;

namespace PlanSphere.Core.Features.Departments.Commands.PatchDepartments;

public record PatchDepartmentCommand(JsonPatchDocument<DepartmentUpdateRequest> PatchDocument) : IRequest
{
    [JsonIgnore]
    public ulong DepartmentId { get; set; }
    public bool InheritAddress { get; set; }
}