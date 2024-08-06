using System.Threading.Tasks;
using ApplicationCore.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(AppIdentityDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (dbContext.Database.IsSqlServer())
            {
                dbContext.Database.Migrate();
            }
            await roleManager.CreateAsync(new IdentityRole(UserType.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserType.Customer.ToString()));
            var defaultUser = new ApplicationUser { UserName = "user@gmail.com", Email = "user@gmail.com" };
            await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);
            defaultUser = await userManager.FindByNameAsync("user@gmail.com");
            if (defaultUser != null)
            {
                await userManager.AddToRoleAsync(defaultUser, UserType.Customer.ToString());
            }

            string adminUserName = "admin@gmail.com";
            var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };
            await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
            adminUser = await userManager.FindByNameAsync(adminUserName);
            if (adminUser != null)
            {
                await userManager.AddToRoleAsync(adminUser, UserType.Administrator.ToString());
            }
        }
    }
}
