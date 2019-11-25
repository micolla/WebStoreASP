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
            services.AddTransient<WebStoreDataInitialize>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, WebStoreDataInitialize webStoreDataInitialize)
        {
            webStoreDataInitialize.InitialAsync().Wait();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
