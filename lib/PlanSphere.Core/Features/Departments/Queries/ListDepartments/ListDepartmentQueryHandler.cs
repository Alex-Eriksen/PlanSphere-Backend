using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Extensions;
using PlanSphere.Core.Features.Departments.DTOs;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Interfaces.Services;

namespace PlanSphere.Core.Features.Departments.Queries.ListDepartments;

[HandlerType(HandlerType.SystemApi)]
public class ListDepartmentQueryHandler (
    IDepartmentRepository departmentRepository,
    ILogger<ListDepartmentQueryHandler> logger,
    IPaginationService paginationService
) : IRequestHandler<ListDepartmentQuery, IPaginatedResponse<DepartmentDTO>>
{
    private readonly IDepartmentRepository _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    private readonly ILogger<ListDepartmentQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPaginationService _paginationService = paginationService ?? throw new ArgumentNullException(nameof(paginationService));

    public async Task<IPaginatedResponse<DepartmentDTO>> Handle(ListDepartmentQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching Departments");
        _logger.LogInformation("Fetching departments from company with id: [{companyId}]", request.CompanyId);
        var query = _departmentRepository.GetQueryable().Where(x => x.CompanyId == request.CompanyId);
        _logger.LogInformation("Fetched departments from company with id: [{companyId}]", request.CompanyId);

        query = SearchQuery(request, query);
        query = SortQuery(request, query);

        var paginatedRespone = await _paginationService.PaginateAsync<Department, DepartmentDTO>(query, request);

        return paginatedRespone;
    }

    private IQueryable<Department> SearchQuery(ListDepartmentQuery request, IQueryable<Department> query)
    {
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower().Trim();
            query = query.Where(d => d.Name.ToLower().Contains(search) ||
                                     (d.Address.StreetName.ToLower() + " " + d.Address.HouseNumber.ToLower()).Contains(search));

        }

        return query;
    }

    private IQueryable<Department> SortQuery(ListDepartmentQuery request, IQueryable<Department> query)
    {
        return request.SortBy switch
        {
            DepartmentSortBy.Name => query.OrderByExpression(x => x.Name, request.SortDescending),
            DepartmentSortBy.StreetName => query.OrderByExpression(x => x.Address.StreetName, request.SortDescending),
            DepartmentSortBy.HouseNumber => query.OrderByExpression(x => x.Address.HouseNumber, request.SortDescending)

        };
    }
}