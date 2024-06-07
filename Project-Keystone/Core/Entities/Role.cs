using Microsoft.AspNetCore.Identity;

namespace Project_Keystone.Core.Entities
{
    public class Role : IdentityRole<int>
    {
        
        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
