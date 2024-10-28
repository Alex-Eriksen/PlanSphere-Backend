using PlanSphere.Core.Abstract;

namespace PlanSphere.Core.Interfaces;

public interface ISearchableQueryHandler<TEntity, in TQuery> where TEntity : BaseEntity where TQuery : ISearchableQuery
{
    IQueryable<TEntity> SearchQuery(string search, IQueryable<TEntity> query);
}
