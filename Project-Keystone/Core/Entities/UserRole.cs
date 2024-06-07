﻿using Microsoft.AspNetCore.Identity;

namespace Project_Keystone.Core.Entities
{
    public class UserRole : IdentityUserRole<int>
    {
       
        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } =  null!;
    }
}