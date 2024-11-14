using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Roles.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Roles.Queries.LookUpRoles;

[HandlerType(HandlerType.SystemApi)]
public class LookUpRolesQueryHandler(
    IRoleRepository roleRepository,
    ILogger<LookUpRolesQueryHandler> logger,
    IMapper mapper
) : IRequestHandler<LookUpRolesQuery, List<RoleLookUpDTO>>
{
    private readonly ILogger<LookUpRolesQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<RoleLookUpDTO>> Handle(LookUpRolesQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Looking up roles.");
        _logger.LogInformation("Retrieving roles from repository.");
        var roles = await _roleRepository.GetRolesLookUp(cancellationToken);
        _logger.LogInformation("Retrieved roles from repository.");

        var roleLookUpDTOs = _mapper.Map<List<RoleLookUpDTO>>(roles);

        return roleLookUpDTOs;
    }
}