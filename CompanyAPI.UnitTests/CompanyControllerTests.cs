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

        private static readonly object[] GetInvalidIsinValues =
        {
            new object[] { "" },
            new object[] { null }
        };

        // Tests
        [Test]
        public async Task GetAll_WhenCalled_ReturnsOkResultListOfCompaniesAsync()
        {
            var companies = new List<Company>
            {
                new Company { Id = 1, Name = "Company 1", StockTicker = "C1", Exchange = "ABC1", Isin = "US1234567890", WebsiteUrl = "https://www.company1.com" },
                new Company { Id = 2, Name = "Company 2", StockTicker = "C2", Exchange = "ABC2", Isin = "US0987654321", WebsiteUrl = "https://www.company2.com" }
            };

            _companyRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(companies);

            var result = await _company.GetAll();

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAll_WhenListOfCompaniesIsEmpty_ReturnNotFound()
        {
            // Arrange: Mocking the repository to return an empty list
            _companyRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Company>());

            var result = await _company.GetAll();

            Assert.That(result.Result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetById_WhenCalled_ReturnsOkResultWithCompany()
        {
            var company = new Company { Id = 1, Name = "Company A", Isin = "US1234567890", Exchange = "ABC", StockTicker = "ABC" };
            _companyRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(company);

            var result = await _company.GetById(1);

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [TestCase(2)]
        [TestCase(-1)]
        public void GetById_WhenInputValueNotFoundOrInvalid_ReturnsBadRequest(int number)
        {
            var company = new Company { Id = 1, Name = "Company A", Isin = "US1234567890", Exchange = "ABC", StockTicker = "ABC" };
            _companyRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(company);

            Assert.That(_company.GetById(number).Result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetByIsin_WhenCalled_ReturnsOkResultWithCompany()
        {
            var company = new Company { Id = 1, Name = "Company A", Isin = "US1234567890", Exchange = "ABC", StockTicker = "ABC" };
            _companyRepositoryMock.Setup(repo => repo.GetByIsinAsync(company.Isin)).ReturnsAsync(company);

            var result = await _company.GetByIsin(company.Isin);

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [TestCase("ABC")]
        [TestCaseSource(nameof(GetInvalidIsinValues))]
        public void GetByIsin_WhenInputValueIsInvalidOrEmptyOrNull_ReturnsBadRequest(string isin)
        {
            Assert.That(_company.GetByIsin(isin).Result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }
    }
}