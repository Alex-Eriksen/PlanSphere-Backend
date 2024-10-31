using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;

public class CreateOrganisationCommandHandler(
    IOrganisationRepository organisationRepository,
    ILogger<CreateOrganisationCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<CreateOrganisationCommand>
{
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));
    private readonly ILogger<CreateOrganisationCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task Handle(CreateOrganisationCommand request, CancellationToken cancellationToken) 
    {
       logger.LogInformation("CreateOrganisationCommandHandler");
       _logger.LogInformation("Creating organisation with name: [{Name}]", request.Name);
        
       var address = _mapper.Map<Domain.Entities.Address>(request.Address);
        
       var organisation = new Organisation
       {
           Name = request.Name,
           Address = address
       };
       await _organisationRepository.CreateAsync(organisation, cancellationToken);
       
       _logger.LogInformation("Organisation created successfully with name: [{Name}]", request.Name); 
    }
}