namespace PlanSphere.Core.Interfaces.Repositories;

public interface IBaseRepository<TEntity, in TIdType>
{
    Task<TEntity> CreateAsync(TEntity request, CancellationToken cancellationToken);
    Task<TEntity> GetByIdAsync(TIdType id, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(TIdType id, TEntity request, CancellationToken cancellationToken);
    Task<TEntity> DeleteAsync(TIdType id, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    IQueryable<TEntity> GetQueryable();
}