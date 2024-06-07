using Microsoft.AspNetCore.Identity;

namespace Project_Keystone.Core.Entities
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            string[] roleNames = { "Admin", "Customer", "Guest" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var role = new Role { Name = roleName };
                    roleResult = await roleManager.CreateAsync(role);
                }
            }
            var adminUser = new User
            {
                UserName = "admin",
                Email = "alex.santexis@gmail.com",
                Firstname = "Admin",
                Lastname = "User",
                EmailConfirmed = true
            };
            var user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(adminUser, "Admin@123");
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
