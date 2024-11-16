using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Extensions;
using PlanSphere.Core.Features.Users.DTOs;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Interfaces.Services;

namespace PlanSphere.Core.Features.Users.Queries.ListUsers;

[HandlerType(HandlerType.SystemApi)]
public class ListUsersQueryHandler(
    IUserRepository userRepository,
    ILogger<ListUsersQueryHandler> logger,
    IPaginationService paginationService
) : IRequestHandler<ListUsersQuery, IPaginatedResponse<UserListDTO>>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly ILogger<ListUsersQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPaginationService _paginationService = paginationService ?? throw new ArgumentNullException(nameof(paginationService));

    public async Task<IPaginatedResponse<UserListDTO>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Starting to List users with [ListUsersQueryHandler]");
        
        _logger.LogInformation("Listing all users on [{sourceLevel}]", request.SourceLevel);
        var query = _userRepository.GetQueryable();
        _logger.LogInformation("Listed all users on [{sourceLevel}]", request.SourceLevel);

        _logger.LogInformation("Making search on listing all users");
        query = SearchQuery(request.Search, query);
        query = SortQuery(request, query);
        _logger.LogInformation("Searched on listing all users");

        _logger.LogInformation("Mapping  listing all users");
        var paginatedResponse = await _paginationService.PaginateAsync<User, UserListDTO>(query, request);
        _logger.LogInformation("Mapped search on listing all users");

        return paginatedResponse;
    }

    private IQueryable<User> SearchQuery(string? search, IQueryable<User> query)
    {
        if (!string.IsNullOrWhiteSpace(search))
        {
            var filteredSearch = search?.Trim().ToLower();
            query = query.Where(x => x.FirstName.ToLower().Contains(filteredSearch));
        }
        return query;
    }

    private IQueryable<User> SortQuery(ListUsersQuery request, IQueryable<User> query)
    {
        return request.SortBy switch
        {
            UserSortBy.FirstName => query.OrderByExpression(u => u.FirstName, request.SortDescending),
            UserSortBy.LastName => query.OrderByExpression(u => u.LastName, request.SortDescending),
            UserSortBy.Role => query.OrderByExpression(u => u.Roles, request.SortDescending),
            UserSortBy.Address => query.OrderByExpression(u => u.Address, request.SortDescending),
            UserSortBy.PhoneNumber => query.OrderByExpression(u => u.PhoneNumber, request.SortDescending),
            UserSortBy.Email => query.OrderByExpression(u => u.Email, request.SortDescending),
            UserSortBy.CreatedAt => query.OrderByExpression(u => u.CreatedAt, request.SortDescending),
            UserSortBy.CreatedBy => query.OrderByExpression(u => u.CreatedByUser, request.SortDescending)
        };
    }
}