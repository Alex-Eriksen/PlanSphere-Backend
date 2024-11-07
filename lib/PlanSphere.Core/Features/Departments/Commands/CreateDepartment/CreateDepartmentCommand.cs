using System.Text.Json.Serialization;
using MediatR;
using PlanSphere.Core.Features.Departments.Request;

namespace PlanSphere.Core.Features.Departments.Commands.CreateDepartment;

public record CreateDepartmentCommand (DepartmentRequest Request) : IRequest
{
    [JsonIgnore]
    public ulong CompanyId { get; set; }
}