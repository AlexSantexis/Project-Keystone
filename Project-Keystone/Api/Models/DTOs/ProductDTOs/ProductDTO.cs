using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Project_Keystone.Api.Models.DTOs.ProductDTOs
{
    public class ProductDTO
    {
        public int? ProductId { get; set; }
        
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
      
        
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
        public List<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
    }
}
