using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;
[HandlerType(HandlerType.SystemApi)]
public class CreateOrganisationCommandHandler(
    IOrganisationRepository organisationRepository,
    ILogger<CreateOrganisationCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<CreateOrganisationCommand>
{
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));
    private readonly ILogger<CreateOrganisationCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task Handle(CreateOrganisationCommand command, CancellationToken cancellationToken) 
    {
       _logger.BeginScope("Starting to create an organistion on [{sourceLevel}] with CreateOrganisationCommandHandler with name: [{organisationName}] with id: [{sourceLevalId}]", command.SourceLevel, command.Request.Name, command.SourceLevelId);
       _logger.LogInformation("Creating organisation on [{sourceLevel}] with name: [{organisationName}] with id: [{sourceLevalId}]", command.SourceLevel, command.Request.Name, command.SourceLevelId);
        
       var organisation = _mapper.Map<Organisation>(command);
        
       await _organisationRepository.CreateAsync(organisation, cancellationToken);
       
       _logger.LogInformation("Organisation created successfully on [{sourceLevel}] with name: [{organisationName}] with id: [{sourceLevalId}]", command.SourceLevel, command.Request.Name, command.SourceLevelId); 
    }
}