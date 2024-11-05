using Domain.Entities;
namespace PlanSphere.Core.Interfaces.Repositories; 
public interface ICompanyRepository : IRepository<Company>
{
    Task<string> UploadLogo(ulong companyId, string fileUrl, CancellationToken cancellationToken);
}