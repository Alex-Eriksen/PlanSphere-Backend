using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Extensions;
using PlanSphere.Core.Features.Organisations.DTOs;
using PlanSphere.Core.Features.Organisations.Queries.ListOrganisations;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Services.Interfaces;

namespace PlanSphere.Core.Features.Organisations.Commands.ListOrganisations;

public class ListOrganisationsQueryHandler(
    IOrganisationRepository organisationRepository,
    ILogger<ListOrganisationsQueryHandler> logger,
    IPaginationService paginationService
) : IRequestHandler<ListOrganisationsQuery, IPaginatedResponse<OrganisationDTO>>
{
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));
    private readonly ILogger<ListOrganisationsQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPaginationService _paginationService = paginationService ?? throw new ArgumentNullException(nameof(paginationService));

    public async Task<IPaginatedResponse<OrganisationDTO>> Handle(ListOrganisationsQuery request, CancellationToken cancellationToken)
    {
        logger.BeginScope("ListOrganisationsCommandHandler");
        
        _logger.LogInformation("Listing all organisations");
        var query = _organisationRepository.GetQueryable();
        _logger.LogInformation("Listed organisations");

        query = SearchQuery(request, query);
        query = SortQuery(request, query);
        
        var paginatedResponse = await _paginationService.PaginateAsync<Organisation, OrganisationDTO>(query, request);

        return paginatedResponse;
    }

    private IQueryable<Organisation> SearchQuery(ListOrganisationsQuery request, IQueryable<Organisation> query)
    {
        var search = request.Search.Trim().ToLower();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x => x.Name.ToLower().Contains(search));
        }

        return query;
    }

    private IQueryable<Organisation> SortQuery(ListOrganisationsQuery request, IQueryable<Organisation> query)
    {
        return request.SortBy switch
        {
            SortByOrganisation.Name => query.OrderByExpression(o => o.Name, request.SortDescending),
            SortByOrganisation.Users => query.OrderByExpression(o => o.Users, request.SortDescending),
            SortByOrganisation.OrganisationMembers => query.OrderByExpression(o => o.Users.Count,
                request.SortDescending),
            SortByOrganisation.CompanyMembers => query.OrderByExpression(
                o => o.Users.SelectMany(x => x.Roles).Count(x => x.Role.CompanyRole != null), request.SortDescending),
            SortByOrganisation.DepartmentMembers => query.OrderByExpression(
                o => o.Users.SelectMany(x => x.Roles).Count(x => x.Role.DepartmentRole != null),
                request.SortDescending),
            SortByOrganisation.TeamMembers => query.OrderByExpression(
                o => o.Users.SelectMany(x => x.Roles).Count(x => x.Role.TeamRole != null), request.SortDescending),
            SortByOrganisation.Roles => query.OrderByExpression(o => o.Roles.Count, request.SortDescending),
        };
    }
}