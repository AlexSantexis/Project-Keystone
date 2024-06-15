using Microsoft.AspNetCore.Identity;

namespace Project_Keystone.Infrastructure.Configurations
{
    public static class RoleConfiguration
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
                string[] roleNames = { "Admin", "User" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                    }
                }
            }
        }
    }
}
