using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Organisations.Requests;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Organisations.Commands.PatchOrganisation;

[HandlerType(HandlerType.SystemApi)]
public class PatchOrganisationCommandHandler(
    IOrganisationRepository organisationRepository,
    ILogger<PatchOrganisationCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<PatchOrganisationCommand>
{
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));
    private readonly ILogger<PatchOrganisationCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));


    public async Task Handle(PatchOrganisationCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Trying to patch organisation with [PatchOrganisationCommandHandler], first getting the organisation then patching");
        _logger.LogInformation("Fetching organisation with id: [{organisationId}]", command.Id);
        var organisation = await _organisationRepository.GetByIdAsync(command.Id, cancellationToken);
        _logger.LogInformation("Fetched organisation with id: [{organisationId}]", command.Id);
        
        _logger.LogInformation("Mapping organisation with id: [{organisationId}]", command.Id);
        var organisationPatchRequest = _mapper.Map<OrganisationUpdateRequest>(organisation);
        
        command.PatchDocument.ApplyTo(organisationPatchRequest);
        
        organisation = _mapper.Map(organisationPatchRequest, organisation);
        
        _logger.LogInformation("Patching organisation with id: [{organisationId}]", command.Id);
        await _organisationRepository.UpdateAsync(command.Id, organisation, cancellationToken);
        _logger.LogInformation("Patched organisation with id: [{organisationId}]", command.Id);
    }
}