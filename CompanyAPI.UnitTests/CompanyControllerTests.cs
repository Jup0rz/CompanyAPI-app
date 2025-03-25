using CompanyAPI.Controllers;
using CompanyAPI.Entities;
using CompanyAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CompanyAPI.UnitTests
{
    [TestFixture]
    public class CompanyControllerTests
    {
        // Private call
        private Mock<ICompanyRepository> _companyRepositoryMock;
        private CompanyController _company;

        // Setup
        [SetUp]
        public void Setup()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _company = new CompanyController(_companyRepositoryMock.Object);
        }

        private static readonly object[] GetInvalidValues =
        {
            new object[] { "" },
            new object[] { " " },
            new object[] { null }
        };

        // Tests
        [Test]
        public void GetAll_WhenCalled_ReturnsOkResultListOfCompaniesAsync()
        {
            var companies = new List<Company>
            {
                new Company { Id = 1, Name = "Company 1", StockTicker = "C1", Exchange = "ABC1", Isin = "AA0000000000", WebsiteUrl = "https://www.company1.com" },
                new Company { Id = 2, Name = "Company 2", StockTicker = "C2", Exchange = "ABC2", Isin = "AA0000000001", WebsiteUrl = "https://www.company2.com" }
            };

            _companyRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(companies);

            var result = _company.GetAll();

            Assert.That(result.Result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetAll_WhenListOfCompaniesIsEmpty_ReturnNotFound()
        {
            // Arrange: Mocking the repository to return an empty list
            _companyRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Company>());

            var result = _company.GetAll();

            Assert.That(result.Result.Result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public void GetById_WhenCalled_ReturnsOkResultWithCompany()
        {
            var company = new Company { Id = 1, Name = "Company A", Isin = "AA0000000000", Exchange = "ABC", StockTicker = "ABC" };
            _companyRepositoryMock.Setup(repo => repo.GetByIdAsync(company.Id)).ReturnsAsync(company);

            var result = _company.GetById(company.Id);

            Assert.That(result.Result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        [TestCase(2)]
        [TestCase(-1)]
        public void GetById_WhenInputValueNotFoundOrInvalid_ReturnsBadRequest(int number)
        {
            var company = new Company { Id = 1, Name = "Company A", Isin = "AA0000000000", Exchange = "ABC", StockTicker = "ABC" };
            _companyRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(company);

            Assert.That(_company.GetById(number).Result.Result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetByIsin_WhenCalled_ReturnsOkResultWithCompany()
        {
            var company = new Company { Id = 1, Name = "Company A", Isin = "AA0000000000", Exchange = "ABC", StockTicker = "ABC" };
            _companyRepositoryMock.Setup(repo => repo.GetByIsinAsync(company.Isin)).ReturnsAsync(company);

            var result = _company.GetByIsin(company.Isin);

            Assert.That(result.Result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        [TestCase("ABC")]
        [TestCaseSource(nameof(GetInvalidValues))]
        public void GetByIsin_WhenInputValueIsInvalidOrEmptyOrNull_ReturnsBadRequest(string isin)
        {
            Assert.That(_company.GetByIsin(isin).Result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        [TestCase("A1")]
        [TestCase("1A")]
        [TestCase("A")]
        [TestCaseSource(nameof(GetInvalidValues))]
        public async Task CreateCompany_WhenInputValueIsInvalidIsin_ReturnsBadRequest(string isin)
        {
            var company = new Company { Isin = isin };

            var result = await _company.CreateCompany(company);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult?.Value, Is.EqualTo("Invalid ISIN format. ISIN must start with two non-numeric characters."));
        }

        [Test]
        public async Task CreateCompany_WhenInputValueIsUniqueIsin_ReturnsBadRequest()
        {
            var company = new Company { Id = 0, Isin = "AA0000000000" };
            _companyRepositoryMock.Setup(repo => repo.IsIsinUnique(company.Isin, company.Id)).ReturnsAsync(true);

            var result = await _company.CreateCompany(company);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult?.Value, Is.EqualTo("A company with the same ISIN already exists."));
        }

        [Test]
        public void CreateCompany_WhenCalled_ReturnsOk()
        {
            var company = new Company { Isin = "AA0000000000" };

            var result = _company.CreateCompany(company);

            Assert.That(result.Result, Is.TypeOf<OkResult>());
        }

        [Test]
        public async Task UpdateCompany_WhenInputValueIsInvalid_ReturnsBadRequest()
        {
            var company = new Company { Isin = "1A" }; // Invalid ISIN

            var result = await _company.UpdateCompany(company);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult?.Value, Is.EqualTo("Invalid ISIN format. ISIN must start with two non-numeric characters."));
        }

        [Test]
        public async Task UpdateCompany_WhenInputValueIsUniqueIsin_ReturnsBadRequest()
        {
            var company = new Company { Id = 0, Isin = "AA0000000000" };
            _companyRepositoryMock.Setup(repo => repo.IsIsinUnique(company.Isin, company.Id)).ReturnsAsync(true);

            var result = await _company.CreateCompany(company);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult?.Value, Is.EqualTo("A company with the same ISIN already exists."));
        }

        [Test]
        public void UpdateCompany_WhenCalled_ReturnsOk()
        {
            var company = new Company { Id = 0, Isin = "AA0000000000" };
            _companyRepositoryMock.Setup(repo => repo.IsIsinUnique(company.Isin, company.Id)).ReturnsAsync(true);

            company.Isin = "AA0000000001";
            var result = _company.UpdateCompany(company);

            Assert.That(result.Result, Is.TypeOf<OkResult>());
        }
    }
}