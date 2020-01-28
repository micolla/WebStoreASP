using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Interfaces.Api;
using WebStore.Interfaces.DataProviders;
using WebStore.Domain.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using WebStore.Services.Database;
using WebStore.Services.DataProviders.CookiesDataProvider;
using WebStore.Clients.Values;
using WebStore.Clients.Employees;
using WebStore.Clients.Products;
using WebStore.Clients.Orders;
using WebStore.Clients.Identity;
using Microsoft.Extensions.Logging;
using WebStore.Logger;

namespace WebStore
{
    public class Startup
    {
        
        IConfiguration Configuration { get; set; }
        public Startup(IConfiguration Config) => Configuration = Config;

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddScoped<IEmployeeService, EmployeesClient>();
            services.AddScoped<IProductService, ProductsClient>();
            services.AddScoped<ICartDataProvider, CookieCartProvider>();
            services.AddScoped<IOrderService, OrdersClient>();
            services.AddTransient<WebStoreDataInitialize>();

            services.AddTransient<IValuesService, ValuesClient>();

            services.AddTransient<IUserStore<User>, UsersClient>();
            services.AddTransient<IUserRoleStore<User>, UsersClient>();
            services.AddTransient<IUserClaimStore<User>, UsersClient>();
            services.AddTransient<IUserPasswordStore<User>, UsersClient>();
            services.AddTransient<IUserEmailStore<User>, UsersClient>();
            services.AddTransient<IUserPhoneNumberStore<User>, UsersClient>();
            services.AddTransient<IUserTwoFactorStore<User>, UsersClient>();
            services.AddTransient<IUserLoginStore<User>, UsersClient>();
            services.AddTransient<IUserLockoutStore<User>, UsersClient>();

            services.AddTransient<IRoleStore<Role>, RolesClient>();

            services.AddIdentity<User, Role>()
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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            logger.AddLog4Net();
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
