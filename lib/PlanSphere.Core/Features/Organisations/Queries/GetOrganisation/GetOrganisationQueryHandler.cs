using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Organisations.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Organisations.Queries.GetOrganisation;

[HandlerType(HandlerType.SystemApi)]
public class GetOrganisationQueryHandler(
    IOrganisationRepository organisationRepository,
    ILogger<GetOrganisationQueryHandler> logger,
    IMapper mapper
) : IRequestHandler<GetOrganisationQuery, OrganisationDetailDTO>
{
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));
    private readonly ILogger<GetOrganisationQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<OrganisationDetailDTO> Handle(GetOrganisationQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Starting to Get organisation with [GetOrganisationQueryHandler]");
        _logger.LogInformation("Fetching organisation with id: [{organisationId}]", request.SourceLevelId);
        var organisation = await _organisationRepository.GetByIdAsync(request.SourceLevelId, cancellationToken);
        _logger.LogInformation("Fetched organisation with id: [{organisationId}]", request.SourceLevelId);

        var mappedOrganisation = _mapper.Map<OrganisationDetailDTO>(organisation);

        return mappedOrganisation;
    }
}