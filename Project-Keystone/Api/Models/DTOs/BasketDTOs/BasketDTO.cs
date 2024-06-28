using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs.BasketDTOs
{
    public class BasketDTO
    {
        public int BasketId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();
    }
}
