using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Roles.Commands.UpdateRole;

[HandlerType(HandlerType.SystemApi)]
public class UpdateRoleCommandHandler(
    ILogger<UpdateRoleCommandHandler> logger,
    IMapper mapper,
    IRoleRepository roleRepository
) : IRequestHandler<UpdateRoleCommand>
{
    private readonly ILogger<UpdateRoleCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Update role");
    }
}