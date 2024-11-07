using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Organisations.Commands.UpdateOrganisation;

[HandlerType(HandlerType.SystemApi)]
public class UpdateOrganisationCommandHandler(
    IOrganisationRepository organisationRepository,
    ILogger<UpdateOrganisationCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<UpdateOrganisationCommand>
{
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));
    private readonly ILogger<UpdateOrganisationCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task Handle(UpdateOrganisationCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Updating organisation with UpdateOrganisationCommandHandler with id: [{organisationId}]",command.SourceLevelId);
        _logger.LogInformation("Fetching organisation with id: [{SourceLevelId}]", command.SourceLevelId);
        var organisation = await _organisationRepository.GetByIdAsync(command.SourceLevelId, cancellationToken);
        _logger.LogInformation("Fetched organisation with id: [{SourceLevelId}]", command.SourceLevelId);
        
        var mappedOrganisation = _mapper.Map(command.OrganisationRequest, organisation);
        
        _logger.LogInformation("Updating organisation with id: [{SourceLevelId}]", command.SourceLevelId);
        await _organisationRepository.UpdateAsync(command.SourceLevelId, mappedOrganisation, cancellationToken);
        _logger.LogInformation("Updated organisation with id: [{SourceLevelId}]", command.SourceLevelId);
    }
}