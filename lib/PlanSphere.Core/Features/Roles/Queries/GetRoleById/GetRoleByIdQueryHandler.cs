using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Roles.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Roles.Queries.GetRoleById;

[HandlerType(HandlerType.SystemApi)]
public class GetRoleByIdQueryHandler(
    ILogger<GetRoleByIdQueryHandler> logger,
    IRoleRepository roleRepository,
    IMapper mapper
) : IRequestHandler<GetRoleByIdQuery, RoleDTO>
{
    private readonly ILogger<GetRoleByIdQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<RoleDTO> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Get role by id");
        _logger.LogInformation("Retrieving role with id: [{roleId}]", request.RoleId);
        var role = await _roleRepository.GetByIdAsync(request.RoleId, cancellationToken);
        _logger.LogInformation("Retrieved role with id: [{roleId}]", request.RoleId);

        var roleDto = _mapper.Map<RoleDTO>(role);
        
        return roleDto;
    }
}