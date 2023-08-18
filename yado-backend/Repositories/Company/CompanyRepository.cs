using Microsoft.EntityFrameworkCore;
using yado_backend.Data;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _dbContext;

        public CompanyRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Company>> GetAllCompany()
        {
            return await _dbContext.Companies.ToListAsync();
        }

        public async Task<bool> InsertCompany(Company company)
        {
            _dbContext.Companies.Add(company);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateCompanyById(int companyId, Company company)
        {
            var existingCompany = await _dbContext.Companies.FirstOrDefaultAsync(c => c.ID == companyId);
            if (existingCompany != null)
            {
                existingCompany.Name = company.Name;
                // Update other properties...

                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }

            return false;
        }

        public async Task<bool> DeleteCompanyById(int companyId)
        {
            var company = await _dbContext.Companies.FindAsync(companyId);
            if (company != null)
            {
                _dbContext.Companies.Remove(company);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }
    }
}
