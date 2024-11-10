using PlanSphere.Core.Constants;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Abstract;

public abstract record BasePaginatedQuery : IPaginatedQuery
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public BasePaginatedQuery()
    {
        PageNumber = 1;
        PageSize = Pagination.DefaultPageSize;
    }

    public BasePaginatedQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > Pagination.MaxPageSize ? Pagination.MaxPageSize : pageSize;
    }
}