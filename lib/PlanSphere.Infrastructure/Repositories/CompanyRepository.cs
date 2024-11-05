using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories
{
    public class CompanyRepository(IPlanSphereDatabaseContext context, ILogger<CompanyRepository> logger) : ICompanyRepository
    {
        private readonly IPlanSphereDatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly ILogger<CompanyRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<Company> CreateAsync(Company request, CancellationToken cancellationToken)
        {
            _context.Companies.Add(request);
            await _context.SaveChangesAsync(cancellationToken);
            return request;
        }

        public async Task<Company> DeleteAsync(ulong id, CancellationToken cancellationToken)
        {
            var company = await _context.Companies.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (company == null) 
            {
                _logger.LogInformation("Could not find company with id: [{companyId}]. Company doesn't exist", id);
                throw new KeyNotFoundException($"Could not find company with id: [{id}]. Company doesn't exist");
            }
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync(cancellationToken);
            return company;
        }

        public async Task<Company> GetByIdAsync(ulong id, CancellationToken cancellationToken)
        {
            var company = await _context.Companies.Include(x => x.Address).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (company == null)
            {
                _logger.LogInformation("Could not find company with the id: [{companyId}]. Company doesn't exist!", id);
                throw new KeyNotFoundException($"Could not find company with id: [{id}]. Company doesn't exist!");
            }
            return company;
        }

        public async Task<Company> UpdateAsync(ulong id, Company request, CancellationToken cancellationToken)
        {
            _context.Companies.Update(request);
            await _context.SaveChangesAsync(cancellationToken);
            return request;
        }

        public IQueryable<Company> GetQueryable()
        {
            return _context.Companies
                .Include(x => x.Address)
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task<string> UploadLogo(ulong companyId, string fileUrl, CancellationToken cancellationToken)
        {
            var company = await _context.Companies.SingleOrDefaultAsync(x => x.Id == companyId, cancellationToken);
            if (company == null)
            {
                _logger.LogInformation("Could not find company with the id: [{companyId}]. Company doesn't exist!", companyId);
                throw new KeyNotFoundException($"Could not find company with id: [{companyId}]. Company doesn't exist!");
            }

            company.LogoUrl = fileUrl;
            _context.Companies.Update(company);
            await _context.SaveChangesAsync(cancellationToken);
            return fileUrl;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
