using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Features.Departments.Commands.CreateDepartment;

[HandlerType(HandlerType.SystemApi)]
public class CreateDepartmentCommandHandler(
    IDepartmentRepository departmentRepository,
    ILogger<CreateDepartmentCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<CreateDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    private readonly ILogger<CreateDepartmentCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Creating Department");
        _logger.LogInformation("Creating department on company with id: [{companyId}]", command.CompanyId);
        var department = _mapper.Map<Department>(command);
        
        department.Settings = new DepartmentSettings
        {
            DefaultWorkSchedule = new WorkSchedule
            {
                IsDefaultWorkSchedule = true
            },
            DefaultRole = new Role
            {
                Name = department.Name + "-default-role",
                DepartmentRoleRights = 
                [
                    new DepartmentRoleRight()
                    {
                        RightId = (ulong) Right.View,
                        Department = department
                    }
                ],
                DepartmentRole = new DepartmentRole
                {
                    Department = department
                }
            }
        };

        var createdDepartment = await _departmentRepository.CreateAsync(department, cancellationToken);
        _logger.LogInformation("Created department company with new id: [{departmentId}] on company with id: [{companyId}]", command.CompanyId, createdDepartment.Id);
    }
}