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

namespace PlanSphere.Core.Features.Companies.Qurries.ListCompanies;

[HandlerType(HandlerType.SystemApi)]
public class ListCompaniesQueryHandler(
    ICompanyRepository companyRepository,
    ILogger<ListCompaniesQueryHandler> logger,
    IPaginationService paginationService
) : IRequestHandler<ListCompaniesQuery, IPaginatedResponse<CompanyDTO>>
{
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly ILogger<ListCompaniesQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
    private readonly IPaginationService _paginationService = paginationService ?? throw new ArgumentNullException(nameof(paginationService));
    public async Task<IPaginatedResponse<CompanyDTO>> Handle(ListCompaniesQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching companies from Organisation with id: [{organisationId}]", request.OrganisationId);
        var query = _companyRepository.GetQueryable().Where(x => x.OrganisationId == request.OrganisationId);
        _logger.LogInformation("Fetched companies from Organisation with id: [{organisationId}]", request.OrganisationId);

        query = SearchQuery(request, query);
        query = SortQuery(request, query);

        var paginatedResponse = await _paginationService.PaginateAsync<Company, CompanyDTO>(query, request);

        return paginatedResponse;

    }

    private IQueryable<Company> SearchQuery(ListCompaniesQuery request, IQueryable<Company> query)
    {
        var search = request.Search.ToLower().Trim();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c => c.Name.Contains(search) || 
            (c.Address.StreetName.ToLower() + " " + c.Address.HouseNumber.ToLower()).Contains(search));
        }

        return query;

    }
    private IQueryable<Company> SortQuery(ListCompaniesQuery request, IQueryable<Company> query)
    {
        return request.SortBy switch
        {
            CompanySortBy.Name => query.OrderByExpression(x => x.Name, request.SortDescending),
            CompanySortBy.Address => query.OrderByExpression(x => x.Address.StreetName, request.SortDescending)
                .ThenByExpression(x => x.Address.HouseNumber, request.SortDescending),
        };
    }
}
