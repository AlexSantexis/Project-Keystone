namespace Project_Keystone.Api.Models.DTOs.Wishlist
{
    public class WishlistDTO
    {
        public int WishlistId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<WishlistItemDTO>? Items { get; set; } 
    }
}
