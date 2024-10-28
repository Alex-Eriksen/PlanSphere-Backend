using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Services.Interfaces;

namespace PlanSphere.Core.Services;

public class PaginationService(IMapper _mapper) : IPaginationService
{
	public async Task<IPaginatedResponse<R>> PaginateAsync<T, R>(IQueryable<T> queryable, int pageSize, int pageNumber, Action<IMappingOperationOptions>? mappingOptions = null)
	{
		var totalCount = queryable.Count();
		var results = await queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

		if (totalCount <= (pageNumber - 1) * pageSize)
		{
			pageNumber = totalCount / pageSize;
		}

		var mappedResults = mappingOptions != null ? _mapper.Map<List<R>>(results, mappingOptions) : _mapper.Map<List<R>>(results);

		return new BasePaginatedResponse<R>
		{
			TotalCount = totalCount,
			CurrentPage = pageNumber,
			TotalPages = Convert.ToInt32(Math.Ceiling(totalCount / (double)pageSize)),
			PageSize = pageSize,
			Results = mappedResults
		};
	}

	public async Task<IPaginatedResponse<R>> PaginateAsync<T, R>(IQueryable<T> queryable, BasePaginatedQuery query, Action<IMappingOperationOptions>? mappingOptions = null)
	{
		return await PaginateAsync<T, R>(queryable, query.PageSize, query.PageNumber, mappingOptions);
	}

	public IPaginatedResponse<R> PaginateList<T, R>(IEnumerable<T> list, BasePaginatedQuery query, Action<IMappingOperationOptions>? mappingOptions = null)
	{
		return PaginateList<T, R>(list, query.PageSize, query.PageNumber, mappingOptions);
	}

	public IPaginatedResponse<R> PaginateList<T, R>(IEnumerable<T> list, int pageSize, int pageNumber, Action<IMappingOperationOptions>? mappingOptions = null)
	{
		var totalCount = list.Count();
		var results = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

		if (totalCount <= (pageNumber - 1) * pageSize)
		{
			pageNumber = totalCount / pageSize;
		}

		var mappedResults = mappingOptions != null ? _mapper.Map<List<R>>(results, mappingOptions) : _mapper.Map<List<R>>(results);

		return new BasePaginatedResponse<R>
		{
			TotalCount = totalCount,
			CurrentPage = pageNumber,
			TotalPages = Convert.ToInt32(Math.Ceiling(totalCount / (double)pageSize)),
			PageSize = pageSize,
			Results = mappedResults
		};
	}
}

