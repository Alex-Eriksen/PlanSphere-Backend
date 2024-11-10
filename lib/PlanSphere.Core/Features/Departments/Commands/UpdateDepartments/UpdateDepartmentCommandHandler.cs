using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Departments.Commands.UpdateDepartments;

public class UpdateDepartmentCommandHandler(
    IDepartmentRepository departmentRepository,
    ILogger<UpdateDepartmentCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    private readonly ILogger<UpdateDepartmentCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(UpdateDepartmentCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching department");
        _logger.LogInformation("Fetching department with id: [{departmentId}]", command.DepartmentId);
        var department = await _departmentRepository.GetByIdAsync(command.DepartmentId, cancellationToken);
        _logger.LogInformation("Fetched department with id: [{departmentId}]", command.DepartmentId);

        var mappedDepartment = _mapper.Map(command, department);

        _logger.LogInformation("Updating Department with id: [{departmentId}]", command.DepartmentId);
        await _departmentRepository.UpdateAsync(command.DepartmentId, mappedDepartment, cancellationToken);
        _logger.LogInformation("Updated Department with id: [{departmentId}]", command.DepartmentId);
    }
}