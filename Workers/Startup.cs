using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Workers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Workers
{
    public class Startup
    {
        //WorkerContext db;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
         
            services.AddScoped<IDbInitializer, DbInitializer>();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            string connectionstrIdentity = Configuration.GetConnectionString("IdentityServer");
            services.AddDbContext<WorkerContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(connection));
            /*   services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                   .AddCookie(options => 
                   {
                       options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                   });*/

            //var optionsBuilder = new DbContextOptionsBuilder<WorkerContext>();

            //var optionsf = optionsBuilder
            //       .UseSqlServer(@"Server=(localdb)\MsSqlLocalDb;Database=Workers;Trusted_Connection=True;")
            //       .Options;
            //db = new WorkerContext(optionsf);

            services.AddIdentity<UserAuthentication, IdentityRole>()
              .AddEntityFrameworkStores<AuthenticationContext>();
            services.AddMvc();
            services.AddScoped(typeof(IEFGenericRepository<>), typeof(EFGenericRepository<>));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
          app.UseAuthentication();
            dbInitializer.Initialize();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                     name: "Account",
                     template: "{controller=Account}/{action}/{id?}"
                );
                routes.MapRoute(
                    name: "Roles",
                    template: "{controller=Roles}/{action}/{id?}"
               );
                routes.MapRoute(
                    name: "Holydays",
                    template: "{controller=Holidays}/{action}/{id?}"
               );
                routes.MapRoute(
                    name: "Person",
                    template: "{controller=Person}/{action}/{id?}"
               );
                routes.MapRoute(
                    name: "Vacation",
                     template: "{controller=Vacation}/{action}/{vacationId?}/{personId?}"
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}


//routes.MapRoute(
//    name: "default_route",
//    template: "{controller}/{action}",
//    defaults: new { controller = "Home", action = "Index" }
//);
