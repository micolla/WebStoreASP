using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Interfaces.DataProviders;
using WebStore.Interfaces.Api;
using WebStore.Services.DataProviders.MSSQLDataProvider;
using WebStore.DAL.SQLDBData;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using WebStore.Services.Database;
using WebStore.Services.DataProviders.CookiesDataProvider;
using WebStore.Clients.Values;
using WebStore.Clients.Employees;

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
            services.AddScoped<IEmployeeService, EmployeesClient>();
            services.AddScoped<IProductDataProvider, ProductDataProvider>();
            services.AddScoped<ICartDataProvider, CookieCartProvider>();
            services.AddScoped<IOrderDataProvider, OrderDataProvider>();
            services.AddTransient<WebStoreDataInitialize>();

            services.AddTransient<IValuesService, ValuesClient>();

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
                    
                    opt.LoginPath = "/Account/Login";
                    opt.LogoutPath = "/Account/Logout";
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
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
