using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Extensions;
using PlanSphere.Core.Features.Companies.DTOs;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Interfaces.Services;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Features.Companies.Queries.ListUserCompanies;

[HandlerType(HandlerType.SystemApi)]
public class ListUserCompaniesQueryHandler(
    ICompanyRepository companyRepository,
    IUserRepository userRepository,
    ILogger<ListUserCompaniesQueryHandler> logger,
    IPaginationService paginationService
) : IRequestHandler<ListUserCompaniesQuery, IPaginatedResponse<CompanyDTO>>
{
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly ILogger<ListUserCompaniesQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPaginationService _paginationService = paginationService ?? throw new ArgumentNullException(nameof(paginationService));

    public async Task<IPaginatedResponse<CompanyDTO>> Handle(ListUserCompaniesQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching Companies");
        _logger.LogInformation("Fetching Companies from user with id: [{userId}]", request.UserId);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        var userRoles = user.Roles.Select(x => x.Role);

        var companyIds = userRoles
            .SelectMany(x => x.OrganisationRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Organisation)
            .SelectMany(x => x.Companies)
            .Select(x => x.Id)
            .ToList();
        
        companyIds.AddRange(userRoles
            .SelectMany(x => x.OrganisationRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Organisation)
            .SelectMany(x => x.Companies)
            .Select(x => x.Id)
            .ToList()
        );
        
        companyIds.AddRange(userRoles
            .SelectMany(x => x.CompanyRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Company.Id)
            .ToList()
        );

        companyIds = companyIds.Distinct().ToList();

        var query = _companyRepository.GetQueryable().Where(x => companyIds.Contains(x.Id));
        _logger.LogInformation("Fetched companies with id: [{userId}]", request.UserId);
        
        query = SearchQuery(request, query);
        query = SortQuery(request, query);

        var paginatedResponse = await _paginationService.PaginateAsync<Company, CompanyDTO>(query, request);

        return paginatedResponse;
    }
    
    private IQueryable<Company> SearchQuery(ListUserCompaniesQuery request, IQueryable<Company> query)
    {
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower().Trim();
            query = query.Where(d => d.Name.ToLower().Contains(search) ||
                                     (d.Address.StreetName.ToLower() + " " + d.Address.HouseNumber.ToLower()).Contains(search));
        }

        return query;
    }

    private IQueryable<Company> SortQuery(ListUserCompaniesQuery request, IQueryable<Company> query)
    {
        return request.SortBy switch
        {
            CompanySortBy.Name => query.OrderByExpression(x => x.Name, request.SortDescending),
            CompanySortBy.StreetName => query.OrderByExpression(x => x.Address.StreetName, request.SortDescending),
            CompanySortBy.HouseNumber => query.OrderByExpression(x => x.Address.HouseNumber, request.SortDescending)
        };
    }
}