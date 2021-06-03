namespace WebStore.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WebStore.Common;
    using WebStore.Data.Models;

    public class UserAdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUserAsync(userManager, GlobalConstants.DefaultAdminUserName);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string userName)
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

                await userManager.SetEmailAsync(user, userName);

                await AddUserToAdminAsync(userManager, GlobalConstants.DefaultAdminUserName);

                result = await userManager.AddPasswordAsync(user, "Admin123");

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

            }

        }

        private static async Task AddUserToAdminAsync(UserManager<ApplicationUser> userManager, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var result = await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
