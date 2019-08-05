using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Workers.Models;
using System.Threading.Tasks;
namespace Workers
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<UserAuthentication> userManager,RoleManager<IdentityRole>roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "Pavel12A_34";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                UserAuthentication admin = new UserAuthentication { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
