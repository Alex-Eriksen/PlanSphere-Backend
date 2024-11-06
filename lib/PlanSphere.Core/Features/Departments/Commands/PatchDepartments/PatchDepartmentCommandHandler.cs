using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Departments.Request;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Departments.Commands.PatchDepartments;

[HandlerType(HandlerType.SystemApi)]
public class PatchDepartmentCommandHandler(
    IDepartmentRepository departmentRepository,
    ILogger<PatchDepartmentCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<PatchDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    private readonly ILogger<PatchDepartmentCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(PatchDepartmentCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching department");
        _logger.LogInformation("Fetching department with id: [{departmentId}]", command.departmentId);
        var department = await _departmentRepository.GetByIdAsync(command.departmentId, cancellationToken);
        _logger.LogInformation("Fetched department with id: [{departmentId}]", command.departmentId);

        var departmentPatchRequest = _mapper.Map<DepartmentUpdateRequest>(department);

        command.PatchDocument.ApplyTo(departmentPatchRequest);

        department = _mapper.Map(departmentPatchRequest, department);
        
        _logger.LogInformation("Patching Department with id: [{departmentId}]", command.departmentId);
        await _departmentRepository.UpdateAsync(command.departmentId, department, cancellationToken);
        _logger.LogInformation("Patched Department with id: [{departmentId}]", command.departmentId);
    }
}