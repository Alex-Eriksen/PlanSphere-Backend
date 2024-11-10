using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Rights.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Rights.Queries.LookUp;

[HandlerType(HandlerType.SystemApi)]
public class LookUpRightsQueryHandler(
    ILogger<LookUpRightsQueryHandler> logger,
    IRoleRepository roleRepository,
    IMapper mapper
) : IRequestHandler<LookUpRightsQuery, List<RightLookUpDTO>>
{
    private readonly ILogger<LookUpRightsQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<RightLookUpDTO>> Handle(LookUpRightsQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Looking up rights.");
        
        _logger.LogInformation("Retrieving rights from repository.");
        var rights = await _roleRepository.GetRightsAsync(cancellationToken);
        _logger.LogInformation("Retrieved rights from repository.");

        var rightLookUpDtos = _mapper.Map<List<RightLookUpDTO>>(rights);

        return rightLookUpDtos;
    }
}