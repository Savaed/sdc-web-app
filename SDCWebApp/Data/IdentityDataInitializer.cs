using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SDCWebApp.Helpers.Constants;

namespace SDCWebApp.Data
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            SeedRoles(roleManager);
            SeddUsers(userManager, configuration);
        }

        public static void SeddUsers(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            string adminUsername = "admin";
            string modUsername = "mod";

            if (userManager.FindByNameAsync(adminUsername).Result is null)
            {
                string adminPassword = configuration[ApiConstants.AdminSeedPassword];
                var admin = new IdentityUser
                {
                    Email = $"{adminUsername}@mail.com",
                    UserName = adminUsername
                };

                userManager.CreateAsync(admin, adminPassword).Wait();
                userManager.AddToRoleAsync(admin, "administrator").Wait();
            }

            if (userManager.FindByNameAsync(modUsername).Result is null)
            {
                string modPassword = configuration[ApiConstants.ModeratorSeedPassword];
                var mod = new IdentityUser
                {
                    Email = $"{modUsername}@mail.com",
                    UserName = modUsername
                };

                userManager.CreateAsync(mod, modPassword).Wait();
                userManager.AddToRoleAsync(mod, "moderator").Wait();
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            const string AdministratorRole = "administrator";
            const string ModeratorRole = "moderator";

            if (!roleManager.RoleExistsAsync(AdministratorRole).Result)
            {
                var adminRole = new IdentityRole(AdministratorRole);
                roleManager.CreateAsync(adminRole).Wait();
            }

            if (!roleManager.RoleExistsAsync(ModeratorRole).Result)
            {
                var modRole = new IdentityRole(ModeratorRole);
                roleManager.CreateAsync(modRole).Wait();
            }
        }
    }
}
