using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs
{
    public class BasketItemDTO
    {
        public int BasketItemId { get; set; }
        [Required]
        public int BasketId { get; set; }
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue,ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        public DateTime AddedAt { get; set; }
        public ProductDTO Product { get; set; }
    }
}
