using MediatR;
using Newtonsoft.Json;
using PlanSphere.Core.Features.Departments.Request;

namespace PlanSphere.Core.Features.Departments.Commands.UpdateDepartments;

public record UpdateDepartmentCommand(DepartmentUpdateRequest Request) : IRequest
{
    [JsonIgnore]
    public ulong departmentId { get; set; }
}