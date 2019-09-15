using Microsoft.AspNetCore.Identity;

namespace SDCWebApp.Data
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeddUsers(userManager);
        }

        public static void SeddUsers(UserManager<IdentityUser> userManager)
        {
            string adminUsername = "admin";
            string modUsername = "mod";

            if (userManager.FindByNameAsync(adminUsername).Result is null)
            {
                string password = "P@$sw0rdAdmin";
                var admin = new IdentityUser
                {
                    Email = $"{adminUsername}@mail.com",
                    UserName = adminUsername
                };

                userManager.CreateAsync(admin, password).Wait();
                userManager.AddToRoleAsync(admin, "administrator").Wait();
            }

            if (userManager.FindByNameAsync(modUsername).Result is null)
            {
                string password = "P@$sw0rdMod";
                var mod = new IdentityUser
                {
                    Email = $"{modUsername}@mail.com",
                    UserName = modUsername
                };

                userManager.CreateAsync(mod, password).Wait();
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
