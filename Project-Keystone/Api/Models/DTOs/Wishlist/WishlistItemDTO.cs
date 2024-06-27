namespace Project_Keystone.Api.Models.DTOs.Wishlist
{
    public class WishlistItemDTO
    {
        public int? ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
