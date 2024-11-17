using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Departments.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Departments.Queries.GetDepartment;

[HandlerType(HandlerType.SystemApi)]
public class GetDepartmentQueryHandler(
    IDepartmentRepository departmentRepository,
    ILogger<GetDepartmentQueryHandler> logger,
    IMapper mapper
) : IRequestHandler<GetDepartmentQuery, DepartmentDTO>
{
    private readonly IDepartmentRepository _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    private readonly ILogger<GetDepartmentQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<DepartmentDTO> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
    {
        logger.BeginScope("Fetching Department");
        logger.LogInformation("Fetchting Department with id: [{Id}]", request.departmentId);
        var department = await _departmentRepository.GetByIdAsync(request.departmentId, cancellationToken);
        logger.LogInformation("Fetched Department with id: [{Id}]", request.departmentId);

        var mappedDepartment = _mapper.Map<DepartmentDTO>(department, opt => opt.Items[MappingKeys.InheritAddress] = false);
        return mappedDepartment;
    }
}