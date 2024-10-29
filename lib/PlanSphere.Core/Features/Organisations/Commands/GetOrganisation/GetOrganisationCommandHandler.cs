using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Organisations.Commands.GetOrganisation;

public class GetOrganisationCommandHandler(
    IOrganisationRepository organisationRepository,
    ILogger<GetOrganisationCommandHandler> logger
) : IRequestHandler<GetOrganisationCommand>
{
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));
    private readonly ILogger<GetOrganisationCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    public async Task Handle(GetOrganisationCommand request, CancellationToken cancellationToken)
    {
        logger.BeginScope("GetOrganisationCommandHandler");
        
        _logger.LogInformation("Geting organisation with id: [{id}]", request.Id);
        await _organisationRepository.GetByIdAsync(request.Id, cancellationToken);
        _logger.LogInformation("Geted organisation with id: [{id}]", request.Id);
    }
}