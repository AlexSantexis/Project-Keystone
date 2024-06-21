using System.ComponentModel.DataAnnotations;
using Project_Keystone.Api.Models.DTOs.ProductDTOs;
using Xunit.Sdk;

namespace Project_Keystone.Api.Models.DTOs
{
    public class OrderDetailDTO
    {
        public int OrderDetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total must be greater than 0")]
        public decimal Total { get; set; }
        public ProductDTO Product { get; set; } = null!;
    }
}
