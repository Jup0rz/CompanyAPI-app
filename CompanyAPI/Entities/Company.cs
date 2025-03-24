using System.ComponentModel.DataAnnotations;

namespace CompanyAPI.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string StockTicker { get; set; }

        [Required]
        [MaxLength(50)]
        public string Exchange { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]{2}[A-Za-z0-9]{10}$", ErrorMessage = "ISIN must start with two letters followed by 10 alphanumeric characters.")]
        public string Isin { get; set; }

        [Url]
        public string? WebsiteUrl { get; set; }
    }
}
