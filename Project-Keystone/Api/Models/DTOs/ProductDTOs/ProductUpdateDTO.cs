using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Project_Keystone.Api.Models.DTOs.ProductDTOs
{
    public class ProductUpdateDTO
    {

        
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;


        
        public decimal Price { get; set; }

        public string? ImgUrl { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        
        public List<int> CategoryIds { get; set; } = new List<int>();
        
        public List<int> GenreIds { get; set; } = new List<int>();
    }
}
