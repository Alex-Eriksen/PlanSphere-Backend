using MediatR;
using PlanSphere.Core.Features.Departments.DTOs;

namespace PlanSphere.Core.Features.Departments.Queries.LookUp;

public record LookUpDepartmentsQuery(ulong UserId) : IRequest<List<DepartmentLookUpDTO>>;
