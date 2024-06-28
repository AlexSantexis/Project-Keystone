using System.ComponentModel.DataAnnotations;
using Project_Keystone.Api.Models.DTOs.ProductDTOs;

namespace Project_Keystone.Api.Models.DTOs.BasketDTOs
{
    public class BasketItemDTO
    {
        public int BasketItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string ImgUrl { get; set; } = string.Empty;
    }
}
