using MediatR;
using PlanSphere.Core.Features.Departments.DTOs;

namespace PlanSphere.Core.Features.Departments.Queries.GetDepartment;

public record GetDepartmentQuery(ulong id) : IRequest<DepartmentDTO>;
