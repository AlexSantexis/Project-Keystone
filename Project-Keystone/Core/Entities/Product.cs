using System.ComponentModel;

namespace Project_Keystone.Core.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } 


        public decimal Price { get; set; }

        public string? ImageUrl { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new HashSet<ProductCategory>();

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
        public virtual ICollection<BasketItem> BasketItems { get; set; } = new HashSet<BasketItem>();
        public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new HashSet<WishlistItem>();
        public virtual ICollection<ProductGenre> ProductGenres { get; set; } = new HashSet<ProductGenre>();
    }
}
