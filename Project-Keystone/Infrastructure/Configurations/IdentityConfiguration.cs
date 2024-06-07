using Microsoft.AspNetCore.Identity;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;

namespace Project_Keystone.Infrastructure.Configurations
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<ProjectKeystoneDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
