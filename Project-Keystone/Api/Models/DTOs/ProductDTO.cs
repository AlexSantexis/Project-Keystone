using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Project_Keystone.Api.Models.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
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
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public CategoryDTO Category { get; set; } = null!;
        public List<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
    }
}
