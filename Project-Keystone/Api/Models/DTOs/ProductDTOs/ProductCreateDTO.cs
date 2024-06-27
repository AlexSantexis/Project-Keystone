using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Project_Keystone.Api.Models.DTOs.ProductDTOs
{
    public class ProductCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public string? ImgUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public List<int> GenreIds { get; set; } = new List<int>();

        [Required]
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
