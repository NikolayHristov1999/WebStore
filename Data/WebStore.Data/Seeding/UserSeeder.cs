namespace WebStore.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using WebStore.Common;
    using WebStore.Data.Models;

    public class UserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUserAsync(userManager, GlobalConstants.DefaultAdminUserName, GlobalConstants.AdministratorRoleName);
            await SeedUserAsync(userManager, GlobalConstants.DemoDealerUser, GlobalConstants.DealerRoleName);
            await SeedUserAsync(userManager, GlobalConstants.DemoClientName);
        }

        private static async Task SeedUserAsync(
            UserManager<ApplicationUser> userManager,
            string userName,
            string roleName = null)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                var result = await userManager.CreateAsync(new ApplicationUser(userName));

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

                user = await userManager.FindByNameAsync(userName);

                result = await userManager.AddPasswordAsync(user, GlobalConstants.DemoPassword);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

                user = await userManager.FindByNameAsync(userName);

                await userManager.SetEmailAsync(user, userName);

                if (roleName != null)
                {
                    result = await userManager.AddToRoleAsync(user, roleName);

                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }
    }
}
