using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Extensions;
using PlanSphere.Core.Features.Organisations.DTOs;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Interfaces.Services;

namespace PlanSphere.Core.Features.Organisations.Queries.ListOrganisations;

[HandlerType(HandlerType.SystemApi)]
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
        _logger.BeginScope("Starting to List Organisations with [ListOrganisationsCommandHandler]");
        
        _logger.LogInformation("Listing all organisations");
        var query = _organisationRepository.GetQueryable();
        _logger.LogInformation("Listed organisations");

        query = SearchQuery(request.Search, query);
        query = SortQuery(request, query);
        
        var paginatedResponse = await _paginationService.PaginateAsync<Organisation, OrganisationDTO>(query, request);

        return paginatedResponse;
    }

    private IQueryable<Organisation> SearchQuery(string? search, IQueryable<Organisation> query)
    {
        if (!string.IsNullOrWhiteSpace(search))
        {
            var filteredSearch = search?.Trim().ToLower();

            query = query.Where(x => x.Name.ToLower().Contains(filteredSearch));
        }

        return query;
    }

    private IQueryable<Organisation> SortQuery(ListOrganisationsQuery request, IQueryable<Organisation> query)
    {
        return request.SortBy switch
        {
            OrganisationSortBy.Name => query.OrderByExpression(o => o.Name, request.SortDescending),
            OrganisationSortBy.Users => query.OrderByExpression(o => o.Users.Count, request.SortDescending),
            OrganisationSortBy.OrganisationMembers => query.OrderByExpression(o => o.Users.Count, request.SortDescending),
            OrganisationSortBy.CompanyMembers => query.OrderByExpression(o => o.Users.SelectMany(x => x.Roles).Count(x => x.Role.CompanyRole != null), request.SortDescending),
            OrganisationSortBy.DepartmentMembers => query.OrderByExpression(o => o.Users.SelectMany(x => x.Roles).Count(x => x.Role.DepartmentRole != null), request.SortDescending),
            OrganisationSortBy.TeamMembers => query.OrderByExpression(o => o.Users.SelectMany(x => x.Roles).Count(x => x.Role.TeamRole != null), request.SortDescending),
        };
    }
}