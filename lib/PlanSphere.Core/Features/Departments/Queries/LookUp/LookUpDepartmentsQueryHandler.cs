using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Departments.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Departments.Queries.LookUp;

[HandlerType(HandlerType.SystemApi)]
public class LookUpDepartmentsQueryHandler(
    ILogger<LookUpDepartmentsQueryHandler> logger,
    IUserRepository userRepository,
    IDepartmentRepository departmentRepository
) : IRequestHandler<LookUpDepartmentsQuery, List<DepartmentLookUpDTO>>
{
    public async Task<List<DepartmentLookUpDTO>> Handle(LookUpDepartmentsQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement the look up for departments when departments can be created.
        return await Task.Run(() => new List<DepartmentLookUpDTO>(), cancellationToken);
    }
}