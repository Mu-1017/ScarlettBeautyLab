using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ScarlettBeautyLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScarlettBeautyLab
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await AddTestUsers(
                services.GetRequiredService <RoleManager<UserRoleEntity>>(),
                services.GetRequiredService<UserManager<UserEntity>>());
        }

        private static async Task AddTestUsers(
                    RoleManager<UserRoleEntity> roleManager, 
                    UserManager<UserEntity> userManager)
        {
            var dataExists = roleManager.Roles.Any() || userManager.Users.Any();

            if (dataExists) return;

            // Add a test role
            await roleManager.CreateAsync(new UserRoleEntity("Admin"));

            // Add a test user
            var user = new UserEntity
            {
                Email = "admin@beautylab.local",
                UserName = "admin@beautylab.local",
                FirstName = "Admin",
                LastName = "Tester",
                Birthday = DateTime.UtcNow
            };

            await userManager.CreateAsync(user, "Supersecret123!!");

            // Put the user in the admin role
            await userManager.AddToRoleAsync(user, "Admin");
            await userManager.UpdateAsync(user);
        }
    }
}
