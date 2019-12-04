using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Model.Interfaces;
using WebStore.DAL.DataProviders.MSSQLDataProvider;
using WebStore.DAL.SQLDBData;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL;
using WebStore.Model.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using WebStore.DAL.DataProviders.CookiesDataProvider;

namespace WebStore
{
    public class Startup
    {
        
        IConfiguration Configuration { get; set; }
        public Startup(IConfiguration Config) => Configuration = Config;

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<WebStoreDBContext>(opt=>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IEmployeeDataProvider, EmployeeDataProvider>();
            services.AddScoped<IProductDataProvider, ProductDataProvider>();
            services.AddScoped<ICartDataProvider, CookieCartProvider>();
            services.AddTransient<WebStoreDataInitialize>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WebStoreDBContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(
                opt =>
                    {
                        opt.Password.RequireDigit = true;
                        opt.Lockout.AllowedForNewUsers = true;
                        opt.Lockout.MaxFailedAccessAttempts = 4;
                        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(55);
                        opt.User.RequireUniqueEmail = false;
                    });

            services.ConfigureApplicationCookie(
                opt=>
                {
                    opt.Cookie.Name = "WebStore-Identity";
                    opt.Cookie.HttpOnly = true;
                    opt.Cookie.Expiration = TimeSpan.FromDays(120);
                    
                    opt.LoginPath = "/Action/Login";
                    opt.LogoutPath = "/Action/Logout";
                    opt.AccessDeniedPath = "/Account/AccessDenided";
                    opt.SlidingExpiration = true;
                }
                );

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, WebStoreDataInitialize webStoreDataInitialize)
        {
            webStoreDataInitialize.InitialAsync().Wait();
            webStoreDataInitialize.IdentityInitialAsync().Wait();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
