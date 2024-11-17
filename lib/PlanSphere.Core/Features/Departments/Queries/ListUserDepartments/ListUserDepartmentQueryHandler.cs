using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Extensions;
using PlanSphere.Core.Features.Departments.DTOs;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Interfaces.Services;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Features.Departments.Queries.ListUserDepartments;

[HandlerType(HandlerType.SystemApi)]
public class ListUserDepartmentQueryHandler(
    IDepartmentRepository departmentRepository,
    IUserRepository userRepository,
    ILogger<ListUserDepartmentQueryHandler> logger,
    IPaginationService paginationService
) : IRequestHandler<ListUserDepartmentQuery, IPaginatedResponse<DepartmentDTO>>
{
    private readonly IDepartmentRepository _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly ILogger<ListUserDepartmentQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPaginationService _paginationService = paginationService ?? throw new ArgumentNullException(nameof(paginationService));

    public async Task<IPaginatedResponse<DepartmentDTO>> Handle(ListUserDepartmentQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching Departments");
        _logger.LogInformation("Fetching departments from user with id: [{userId}]", request.UserId);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        var userRoles = user.Roles.Select(x => x.Role);
        
        var departmentIds = userRoles
            .SelectMany(x => x.OrganisationRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Organisation)
            .SelectMany(x => x.Companies)
            .SelectMany(x => x.Departments)
            .Select(x => x.Id)
            .ToList();
        
        departmentIds.AddRange(userRoles
            .SelectMany(x => x.CompanyRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Company)
            .SelectMany(x => x.Departments)
            .Select(x => x.Id)
            .ToList()
        );
        
        departmentIds.AddRange(userRoles
            .SelectMany(x => x.DepartmentRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Department.Id)
            .ToList()
        );

        departmentIds = departmentIds.Distinct().ToList();
        
        var query = _departmentRepository.GetQueryable().Where(x => departmentIds.Contains(x.Id));
        _logger.LogInformation("Fetched departments from user with id: [{userId}]", request.UserId);

        query = SearchQuery(request, query);
        query = SortQuery(request, query);

        var paginatedRespone = await _paginationService.PaginateAsync<Department, DepartmentDTO>(query, request,opt => opt.Items[MappingKeys.InheritAddress] = true);

        return paginatedRespone;
    }
    
    
    private IQueryable<Department> SearchQuery(ListUserDepartmentQuery request, IQueryable<Department> query)
    {
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower().Trim();
            query = query.Where(d => d.Name.ToLower().Contains(search) ||
                                     (d.Address.StreetName.ToLower() + " " + d.Address.HouseNumber.ToLower()).Contains(search));
        }

        return query;
    }

    private IQueryable<Department> SortQuery(ListUserDepartmentQuery request, IQueryable<Department> query)
    {
        return request.SortBy switch
        {
            DepartmentSortBy.Name => query.OrderByExpression(x => x.Name, request.SortDescending),
            DepartmentSortBy.StreetName => query.OrderByExpression(x => x.Address.StreetName, request.SortDescending),
            DepartmentSortBy.HouseNumber => query.OrderByExpression(x => x.Address.HouseNumber, request.SortDescending)
        };
    }
    
}