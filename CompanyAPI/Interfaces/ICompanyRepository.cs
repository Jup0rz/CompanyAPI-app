using CompanyAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllAsync();
        Task<Company> GetByIdAsync(int id);
        Task<Company> GetByIsinAsync(string isin);
        Task<ActionResult> CreateAsync(Company company);
        Task<ActionResult> UpdateAsync(Company company);
    }
}
