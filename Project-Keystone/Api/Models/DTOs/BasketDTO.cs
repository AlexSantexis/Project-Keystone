using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs
{
    public class BasketDTO
    {
        public int BasketId { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }

        public List<BasketItemDTO> BasketItems { get; set; } = new List<BasketItemDTO>();
    }
}
