using CompanyAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    //https://localhost:7070 / api / Company / 
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetAll()
        {
            var companies = new List<Company> {
            new Company
            {
                Id = 1,
                Name = "Test",
                StockTicker = "T1",
                Exchange = "USD",
                Isin = "US123",
                WebsiteUrl = "",
            }
            };

            return Ok(companies);
        }
    }
}
