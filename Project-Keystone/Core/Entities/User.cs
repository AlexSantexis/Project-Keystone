using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Keystone.Core.Entities
{
    public class User : IdentityUser
    {
        public string? Firstname { get; set; } 
        public string? Lastname { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual Basket? Basket { get; set; } 
        public virtual Wishlist? Wishlist { get; set; }

        public virtual Address? Address { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        

    }
}
