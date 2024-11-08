using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Extensions;
using PlanSphere.Core.Features.Departments.DTOs;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Interfaces.Services;
using Serilog;

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
        var query = _departmentRepository.GetQueryable();
        var departmentIds = user.Roles.Select(mpanies.SelectMany(x => x.Departments).Select(x => x.Id).ToList());
        departmentIds.AddRange(user.);
        _logger.LogInformation("Fetched departments from company with id: [{userId}]", request.UserId);

        query = SearchQuery(request, query);
        query = SortQuery(request, query);

        var paginatedRespone = await _paginationService.PaginateAsync<Department, DepartmentDTO>(query, request);

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
            DepartmentSortBy.Address => query.OrderByExpression(x => x.Address.StreetName, request.SortDescending)
                .ThenByExpression(x => x.Address.HouseNumber, request.SortDescending)
        };
    }
    
}