using MediatR;

namespace PlanSphere.Core.Features.Departments.Commands.DeleteDepartment;

public record DeleteDepartmentCommand (ulong Id) : IRequest;