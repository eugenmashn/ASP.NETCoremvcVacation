using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Workers.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
namespace Workers
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Run();
            var host = CreateWebHostBuilder(args);
            using (var scope = host.Services.CreateScope())
            {
                Task t;
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<UserAuthentication>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                   t= RoleInitializer.InitializeAsync(userManager, rolesManager);
                    t.Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "an eeror occurred while seeding the database");
                }
            }
            host.Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().Build();
    }
}
