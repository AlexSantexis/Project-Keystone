namespace Project_Keystone.Core.Entities
{
    public class WishlistItem
    {

        public int WishlistItemId { get; set; }

        public int WishlistId { get; set; }

        public virtual Wishlist Wishlist { get; set; }  = null!;

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }  = null!;

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        
    }
}
