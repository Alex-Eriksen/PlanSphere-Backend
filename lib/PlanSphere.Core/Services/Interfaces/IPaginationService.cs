using AutoMapper;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Services.Interfaces;

public interface IPaginationService
{
    public Task<IPaginatedResponse<R>> PaginateAsync<T, R>(IQueryable<T> queryable, int pageSize, int pageNumber, Action<IMappingOperationOptions>? mappingOptions = null);
    public Task<IPaginatedResponse<R>> PaginateAsync<T, R>(IQueryable<T> queryable, BasePaginatedQuery query, Action<IMappingOperationOptions>? mappingOptions = null);
    public IPaginatedResponse<R> PaginateList<T, R>(IEnumerable<T> list, BasePaginatedQuery query, Action<IMappingOperationOptions>? mappingOptions = null);
    public IPaginatedResponse<R> PaginateList<T, R>(IEnumerable<T> list, int pageSize, int pageNumber, Action<IMappingOperationOptions>? mappingOptions = null);
}
