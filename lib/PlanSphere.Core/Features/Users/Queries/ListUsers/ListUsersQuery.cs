using MediatR;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Features.Users.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Users.Queries.ListUsers;

public record ListUsersQuery(string? Search, UserSortBy SortBy, bool SortDescending) : BasePaginatedQuery,
    IRequest<IPaginatedResponse<UserListDTO>>, ISearchableQuery, ISortableQuery<UserSortBy>;
