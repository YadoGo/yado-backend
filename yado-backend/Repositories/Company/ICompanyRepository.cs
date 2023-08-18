using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompany();
        Task<bool> InsertCompany(Company company);
        Task<bool> UpdateCompanyById(int companyId, Company company);
        Task<bool> DeleteCompanyById(int companyId);
    }
}
