using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Workers.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Workers
{
    public class DbInitializer: IDbInitializer
    {
        private readonly IServiceProvider _serviceProvider;

        public DbInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async void Initialize()
        {
            using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                //create database schema if none exists
                    var context = serviceScope.ServiceProvider.GetService<AuthenticationContext>();
                    context.Database.EnsureCreated(); 

                string adminEmail = "admin@gmail.com";
                string password = "Pavel12A_34";
                    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                    var userManager = serviceScope.ServiceProvider.GetService<UserManager<UserAuthentication>>();
                if (await roleManager.FindByNameAsync("admin") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("admin"));
                }
                if (await roleManager.FindByNameAsync("user") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("user"));
                }
                if (await roleManager.FindByNameAsync("employee") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("employee"));
                }
                if (await userManager.FindByNameAsync(adminEmail) == null)
                {
                    UserAuthentication admin = new UserAuthentication { Email = adminEmail, UserName = adminEmail };
                    IdentityResult result = await userManager.CreateAsync(admin, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "admin");
                    }
            }   }
        }
    }
}
