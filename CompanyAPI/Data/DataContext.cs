using CompanyAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
    }
}
