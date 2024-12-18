﻿using Domain.Entities;
namespace PlanSphere.Core.Interfaces.Repositories; 
public interface ICompanyRepository : IRepository<Company>
{
    Task<string> UploadLogoAsync(ulong companyId, string fileUrl, CancellationToken cancellationToken);
}