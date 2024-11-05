using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Extensions;
using PlanSphere.Core.Features.Roles.DTOs;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Interfaces.Services;

namespace PlanSphere.Core.Features.Roles.Queries.ListRoles;

[HandlerType(HandlerType.SystemApi)]
public class ListRolesQueryHandler(
    IRoleRepository roleRepository,
    ILogger<ListRolesQueryHandler> logger,
    IPaginationService paginationService
) : IRequestHandler<ListRolesQuery, IPaginatedResponse<RoleListItemDTO>>
{
    private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    private readonly ILogger<ListRolesQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPaginationService _paginationService = paginationService ?? throw new ArgumentNullException(nameof(paginationService));

    public async Task<IPaginatedResponse<RoleListItemDTO>> Handle(ListRolesQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("List roles on {sourceLevel}", request.SourceLevel);
        
        _logger.LogInformation("Fetching roles on {sourceLevel} with id: [{sourceLevelId}]", request.SourceLevel, request.SourceLevelId);
        var query = GetRoles(request);
        _logger.LogInformation("Fetched roles on {sourceLevel} with id: [{sourceLevelId}]", request.SourceLevel, request.SourceLevelId);

        query = SearchQuery(request.Search, query);
        query = SortQuery(request, query);

        var paginatedResponse = await _paginationService.PaginateAsync<Role, RoleListItemDTO>(query, request);

        return paginatedResponse;
    }

    private IQueryable<Role> GetRoles(ListRolesQuery request)
    {
        var query = _roleRepository.GetQueryable();
    
        query = request.SourceLevel switch
        {
            SourceLevel.Organisation => query.Where(x => x.OrganisationRole != null && x.OrganisationRole.OrganisationId == request.SourceLevelId), 
            
            SourceLevel.Company => query.Where(x => x.CompanyRole != null && x.CompanyRole.CompanyId == request.SourceLevelId ||
                                                    x.OrganisationRole.OrganisationId == request.OrganisationId && x.OrganisationRole.IsInheritanceActive),
            
            SourceLevel.Department => query.Where(x => x.DepartmentRole != null && x.DepartmentRole.DepartmentId == request.SourceLevelId ||
                                                       x.OrganisationRole != null && x.OrganisationRole.OrganisationId == request.OrganisationId && x.OrganisationRole.IsInheritanceActive ||
                                                       x.CompanyRole != null && x.CompanyRole.Company.OrganisationId == request.OrganisationId && x.CompanyRole.IsInheritanceActive),
            
            SourceLevel.Team => query.Where(x => x.TeamRole != null && x.TeamRole.TeamId == request.SourceLevelId ||
                                                 x.OrganisationRole != null && x.OrganisationRole.OrganisationId == request.OrganisationId && x.OrganisationRole.IsInheritanceActive ||
                                                 x.CompanyRole != null && x.CompanyRole.Company.OrganisationId == request.OrganisationId && x.CompanyRole.IsInheritanceActive ||
                                                 x.DepartmentRole != null && x.DepartmentRole.Department.Company.OrganisationId == request.OrganisationId && x.DepartmentRole.IsInheritanceActive),
            _ => throw new ArgumentOutOfRangeException(nameof(SourceLevel), request.SourceLevel, null)
        };
        
        return query;
    }
    
    private IQueryable<Role> SearchQuery(string? search, IQueryable<Role> query)
    {
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(r => r.Name.Contains(search) ||
                                     r.CreatedByUser.FirstName.Contains(search) ||
                                     r.CreatedByUser.LastName.Contains(search));
        }

        return query;
    }
    
    private IQueryable<Role> SortQuery(ListRolesQuery request, IQueryable<Role> query)
    {
        
        return request.SortBy switch
        {
            RoleSortBy.Name => query.OrderByExpression(r => r.Name, request.SortDescending),
            RoleSortBy.IsInheritanceActive => SortByIsInheritanceActive(request, query),
            RoleSortBy.CreatedAt => query.OrderByExpression(r => r.CreatedAt, request.SortDescending),
            RoleSortBy.CreatedBy => query.OrderByExpression(r => r.CreatedBy, request.SortDescending),
            _ => throw new ArgumentOutOfRangeException(nameof(RoleSortBy), request.SortBy, null)
        };
    }

    private IQueryable<Role> SortByIsInheritanceActive(ListRolesQuery request, IQueryable<Role> query)
    {
        return request.SourceLevel switch
        {
            SourceLevel.Organisation => query.OrderByExpression(x => x.OrganisationRole.IsInheritanceActive, request.SortDescending ),
            SourceLevel.Company => query.OrderByExpression(x => x.CompanyRole.IsInheritanceActive, request.SortDescending ),
            SourceLevel.Department => query.OrderByExpression(x => x.DepartmentRole.IsInheritanceActive, request.SortDescending ),
            SourceLevel.Team => query.OrderByExpression(x => x.TeamRole.IsInheritanceActive, request.SortDescending ),
            _ => throw new ArgumentOutOfRangeException(nameof(SourceLevel), request.SourceLevel, null)
        };
    }
}