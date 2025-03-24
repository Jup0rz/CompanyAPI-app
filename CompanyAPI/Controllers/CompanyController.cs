using CompanyAPI.Entities;
using CompanyAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<Company>>> GetAll()
        {
            var companies = await _companyRepository.GetAllAsync();
            return Ok(companies);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Company>> GetByIdA(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            if (company is null)
                return BadRequest("Id given for company not found.");

            return Ok(company);
        }

        [HttpGet("isin/{isin}")]
        public async Task<ActionResult<Company>> GetByIsin(string isin)
        {
            var company = await _companyRepository.GetByIsinAsync(isin);

            if (company is null)
                return BadRequest("Isin given for company not found.");

            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany(Company company)
        {
            try
            {
                await _companyRepository.CreateAsync(company);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Error creating company.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCompany(Company company)
        {
            try
            {
                await _companyRepository.UpdateAsync(company);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Error updating company.");
            }
        }
    }
}
