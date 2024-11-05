using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Roles.Commands.ToggleInheritance;

[HandlerType(HandlerType.SystemApi)]
public class ToggleRoleInheritanceCommandHandler(
    ILogger<ToggleRoleInheritanceCommandHandler> logger,
    IRoleRepository roleRepository
) : IRequestHandler<ToggleRoleInheritanceCommand>
{
    private readonly ILogger<ToggleRoleInheritanceCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));

    public async Task Handle(ToggleRoleInheritanceCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Toggle role inheritance");
        var role = await _roleRepository.GetByIdAsync(request.RoleId, cancellationToken);

        if (role.SourceLevel == request.SourceLevel)
        {
            _logger.LogInformation("Toggling inheritance on source entity.");
            await _roleRepository.ToggleRoleInheritanceAsync(role, cancellationToken);
            _logger.LogInformation("Toggled inheritance on source entity.");
        }
        else
        {
            _logger.LogInformation("Toggling blocker inheritance.");
            ToggleBlockedRole(request, role);
            await _roleRepository.UpdateAsync(role.Id, role, cancellationToken);
            _logger.LogInformation("Toggled blocker inheritance.");
        }
    }

    private void ToggleBlockedRole(ISourceLevelRequest command, Role role)
    {
        switch (command.SourceLevel)
        {
            case SourceLevel.Company:
                var companyBlockedRole = role.BlockedCompanies.SingleOrDefault(x => x.CompanyId == command.SourceLevelId && x.RoleId == role.Id);
                if (companyBlockedRole != null)
                {
                    role.BlockedCompanies.Remove(companyBlockedRole);
                    return;
                }
                role.BlockedCompanies.Add(new CompanyBlockedRole{CompanyId = command.SourceLevelId});
                break;
            case SourceLevel.Department:
                var departmentBlockedRole = role.BlockedDepartments.SingleOrDefault(x => x.DepartmentId == command.SourceLevelId && x.RoleId == role.Id);
                if (departmentBlockedRole != null)
                {
                    role.BlockedDepartments.Remove(departmentBlockedRole);
                    return;
                }
                role.BlockedDepartments.Add(new DepartmentBlockedRole{DepartmentId = command.SourceLevelId});
                break;
            default: throw new ArgumentOutOfRangeException();
        }
    }
}