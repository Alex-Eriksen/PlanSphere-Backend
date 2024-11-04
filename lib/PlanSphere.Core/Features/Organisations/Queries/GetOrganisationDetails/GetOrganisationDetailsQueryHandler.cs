using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Organisations.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Organisations.Queries.GetOrganisationDetails;

[HandlerType(HandlerType.SystemApi)]
public class GetOrganisationDetailsQueryHandler(
    IOrganisationRepository organisationRepository,
    ILogger<GetOrganisationDetailsQueryHandler> logger,
    IMapper mapper
) : IRequestHandler<GetOrganisationDetailsQuery, OrganisationDetailDTO>
{
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));
    private readonly ILogger<GetOrganisationDetailsQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));


    public async Task<OrganisationDetailDTO> Handle(GetOrganisationDetailsQuery request, CancellationToken cancellationToken)
    {
        logger.BeginScope("Fetching organisation with id: [{organisationId}]", request.Id);
        var organisation = await _organisationRepository.GetByIdAsync(request.Id, cancellationToken);
        _logger.LogInformation("Fetched organisation with id: [{organisationId}]", request.Id);

        var mappedOrganisation = _mapper.Map<OrganisationDetailDTO>(organisation);

        return mappedOrganisation;
    }
}