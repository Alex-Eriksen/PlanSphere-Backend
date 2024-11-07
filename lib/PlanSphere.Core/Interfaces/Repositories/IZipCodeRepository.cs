using Domain.Entities;

namespace PlanSphere.Core.Interfaces.Repositories;

public interface IZipCodeRepository : IRepository<ZipCode>
{
    Task<List<ZipCode>> GetZipCodeLookUpsAsync(CancellationToken cancellationToken);
}