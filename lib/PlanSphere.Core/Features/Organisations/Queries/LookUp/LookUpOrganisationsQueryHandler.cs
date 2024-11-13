using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Organisations.DTOs;
using PlanSphere.Core.Interfaces.Repositories;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Features.Organisations.Queries.LookUp;

[HandlerType(HandlerType.SystemApi)]
public class LookUpOrganisationsQueryHandler(
    IOrganisationRepository organisationRepository,
    IMapper mapper,
    ILogger<LookUpOrganisationsQueryHandler> logger,
    IUserRepository userRepository
) : IRequestHandler<LookUpOrganisationsQuery, List<OrganisationLookUpDTO>>
{
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<LookUpOrganisationsQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<List<OrganisationLookUpDTO>> Handle(LookUpOrganisationsQuery query, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Looking up organisations");
        _logger.LogInformation("Retrieving organisations.");
        var organisations = await _organisationRepository.GetQueryable().ToListAsync(cancellationToken);
        var user = await _userRepository.GetByIdAsync(query.UserId, cancellationToken);
        _logger.LogInformation("Retrieved organisations.");

        var organisationsToAdd = new List<Organisation>();

        foreach (var role in user.Roles.Select(x => x.Role))
        {
            organisationsToAdd.AddRange(organisations.Where(x =>
                role.OrganisationRoleRights
                    .Where(z => z.Right.AsEnum <= Right.View)
                    .Select(y => y.OrganisationId)
                    .Contains(x.Id)
            ).ToList());
        }

        organisationsToAdd = organisationsToAdd.Distinct().ToList();

        var organisationLookUpDtos = _mapper.Map<List<OrganisationLookUpDTO>>(organisationsToAdd);

        return organisationLookUpDtos;
    }
}