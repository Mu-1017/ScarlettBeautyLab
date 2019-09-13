using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ScarlettBeautyLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            await AddTestDatas(services.GetRequiredService<BeautyLabDbContext>());
        }

        private static async Task AddTestDatas(BeautyLabDbContext context)
        {
            if(context.Images.Any() || context.Brands.Any() || context.Products.Any())
            {
                return;
            }

            var imageId1ForBrand1 = Guid.NewGuid();
            await context.Images.AddAsync(new ImageEntity
            {
                Id = imageId1ForBrand1,
                Image = DiorImage(),
                ImageContentType = "image/jpeg"
            });
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
                Nickname = "Admin",
                AgeGroup = SkinAgeGroups.Big30s,
                SkinType = SkinTypes.Normal
            };

            await userManager.CreateAsync(user, "Supersecret123!!");
            // Put the user in the admin role
            await userManager.AddToRoleAsync(user, "Admin");
            await userManager.UpdateAsync(user);

            // Add a test user
            var guest = new UserEntity
            {
                Email = "guest@beautylab.local",
                UserName = "guest@beautylab.local",
                Nickname = "Guest",
                AgeGroup = SkinAgeGroups.LateTeen,
                SkinType = SkinTypes.Oily
            };

            await userManager.CreateAsync(guest, "Supersecret123!!");
            // Put the user in the admin role
            await userManager.AddToRoleAsync(guest, "Guest");
            await userManager.UpdateAsync(guest);

        }


        private static byte[] DiorImage()
        {
            var diorLogo = "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";

            byte[] image = Encoding.ASCII.GetBytes(diorLogo);
            return image;
        }
    }
}
