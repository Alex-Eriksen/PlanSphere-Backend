using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Departments.Commands.DeleteDepartment;

[HandlerType(HandlerType.SystemApi)]
public class DeleteDepartmentCommandHandler(
    IDepartmentRepository departmentRepository,
    ILogger<DeleteDepartmentCommandHandler> logger
) : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    private readonly ILogger<DeleteDepartmentCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Deleting Department");
        _logger.LogInformation("Deleting Department with id [{id}]", request.departmentId);
        await _departmentRepository.DeleteAsync(request.departmentId, cancellationToken);
        _logger.LogInformation("Deleted Department with id [{id}]!", request.departmentId);
    }
}