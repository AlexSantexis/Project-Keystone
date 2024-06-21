using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Project_Keystone.Api.Models.DTOs.ProductDTOs
{
    public class ProductUpdateDTO
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = null!;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public List<int> GenreIds { get; set; } = new List<int>();
    }
}
