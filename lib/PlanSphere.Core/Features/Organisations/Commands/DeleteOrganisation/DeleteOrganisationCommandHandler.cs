using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Organisations.Commands.DeleteOrganisation;
[HandlerType(HandlerType.SystemApi)]
public class DeleteOrganisationCommandHandler(
    IOrganisationRepository organisationRepository,
    ILogger<DeleteOrganisationCommandHandler> logger
) : IRequestHandler<DeleteOrganisationCommand>
{
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));
    private readonly ILogger<DeleteOrganisationCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    public async Task Handle(DeleteOrganisationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete organisation with id: [{id}]", request.Id);

        // try
        // {
        //     // Check if the organisation exists before attempting to delete
        //     var organisationExists = await _organisationRepository.GetByIdAsync(request.Id, cancellationToken);
        //     if (organisationExists == null)
        //     {
        //         _logger.LogWarning("Organisation with id: [{id}] does not exist.", request.Id);
        //         return; // Handle this according to your design (e.g., throw an exception or return a specific result)
        //     }
        //
        //     // Proceed to delete the organisation
        //     _logger.LogInformation("Deleting organisation with id: [{id}]", request.Id);
        //     await _organisationRepository.DeleteAsync(request.Id, cancellationToken);
        //     
        //     if (organisationExists == null)
        //     {
        //         _logger.LogInformation("Successfully deleted organisation with id: [{id}]", request.Id);
        //         return; // Handle this according to your design (e.g., throw an exception or return a specific result)
        //     }
        //
        //     if (organisationExists != null)
        //     {
        //         _logger.LogWarning("Organisation with id: [{id}] did not delete", request.Id);
        //     }
        // }
        // catch (Exception ex)
        // {
        //     _logger.LogError(ex, "An error occurred while trying to delete organisation with id: [{id}]", request.Id);
        //     throw; // Optionally rethrow or handle accordingly
        // }
        
        _logger.BeginScope("DeleteOrganisationCommandHandler");
        
        _logger.LogInformation("Deleting organisation with id: [{id}]", request.Id);
        await _organisationRepository.DeleteAsync(request.Id, cancellationToken);
        _logger.LogInformation("Deleted organisation with id: [{id}]", request.Id);
    }
}