using Domain.Entities;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    public Task<Department> CreateAsync(Department request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Department> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Department> UpdateAsync(ulong id, Department request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Department> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Department> GetQueryable()
    {
        throw new NotImplementedException();
    }
}