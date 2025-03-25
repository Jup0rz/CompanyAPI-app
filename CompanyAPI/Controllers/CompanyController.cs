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
            if (companies.Count == 0 || companies == null)
            {
                return NotFound("Couldn't get list of companies.");
            }

            return Ok(companies);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Company>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Valid Id is required.");

            var company = await _companyRepository.GetByIdAsync(id);

            if (company is null)
                return BadRequest("Id given for company not found.");

            return Ok(company);
        }

        [HttpGet("isin/{isin}")]
        public async Task<ActionResult<Company>> GetByIsin(string isin)
        {
            if (string.IsNullOrEmpty(isin))
            {
                return BadRequest("Isin is required.");
            }

            var company = await _companyRepository.GetByIsinAsync(isin);

            if (company is null)
                return BadRequest("Isin given for company not found.");

            return Ok(company);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateCompany(Company company)
        {
            if (!IsIsinValid(company.Isin))
                return BadRequest("Invalid ISIN format. ISIN must start with two non-numeric characters.");

            if (!IsIsinUnique(company.Isin, company.Id))
                return BadRequest("A company with the same ISIN already exists.");


            await _companyRepository.CreateAsync(company);

            return Ok();
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateCompany(Company company)
        {
            if (!IsIsinValid(company.Isin))
                return BadRequest("Invalid ISIN format. ISIN must start with two non-numeric characters.");

            if (!IsIsinUnique(company.Isin, company.Id))
                return BadRequest("A company with the same ISIN already exists.");

            await _companyRepository.UpdateAsync(company);

            return Ok();
        }

        private static bool IsIsinValid(string isin)
        {
            return !string.IsNullOrWhiteSpace(isin) && isin.Length >= 2 && char.IsLetter(isin[0]) && char.IsLetter(isin[1]);
        }

        private bool IsIsinUnique(string isin, int id)
        {
            return _companyRepository.IsIsinUnique(isin, id).Result;
        }
    }
}
