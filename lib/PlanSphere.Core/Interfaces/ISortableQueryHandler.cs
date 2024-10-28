using PlanSphere.Core.Abstract;

namespace PlanSphere.Core.Interfaces;

public interface ISortableQueryHandler<TEntity, TQuery, TSortByEnum> where TEntity : BaseEntity where TQuery : ISortableQuery<TSortByEnum> where TSortByEnum : Enum
{
    IQueryable<TEntity> SortQuery(TQuery request, IQueryable<TEntity> query);
}
