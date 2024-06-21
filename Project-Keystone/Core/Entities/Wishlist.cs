namespace Project_Keystone.Core.Entities
{
    public class Wishlist
    {
        public int WishlistId { get; set; }

        public string? UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<WishlistItem> WishListItems { get; set; } = new HashSet<WishlistItem>();
    }
}
