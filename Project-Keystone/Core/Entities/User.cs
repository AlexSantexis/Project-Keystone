using Microsoft.AspNetCore.Identity;

namespace Project_Keystone.Core.Entities
{
    public class User : IdentityUser<int>
    {
        
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual Basket? Basket { get; set; } 
        public virtual Wishlist? Wishlist { get; set; }

        public virtual Address? Address { get; set; }
       
    }
}
