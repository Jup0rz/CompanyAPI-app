using CompanyAPI.Data;
using CompanyAPI.Entities;
using CompanyAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _dataContext;
        public CompanyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ActionResult> CreateAsync(Company company)
        {
            var result = await _dataContext.Companies.AddAsync(company);
            await _dataContext.SaveChangesAsync();
            return await Task.FromResult<ActionResult>(new OkResult());
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _dataContext.Companies.ToListAsync();
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _dataContext.Companies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Company> GetByIsinAsync(string isin)
        {
            return await _dataContext.Companies.FirstOrDefaultAsync(x => x.Isin == isin);
        }

        public async Task<ActionResult> UpdateAsync(Company company)
        {
            var existingCompany = await _dataContext.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == company.Id);

            if (existingCompany == null)
            {
                return new NotFoundResult();
            }

            // Update record with inserted values
            _dataContext.Companies.Update(company);

            await _dataContext.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<bool> IsIsinUnique(string isin, int id)
        {
            return !await _dataContext.Companies.AnyAsync(c => c.Isin == isin && c.Id != id);
        }
    }
}
