using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebStore.DAL.SQLDBData;
using WebStore.Interfaces.DataProviders;
using WebStore.Services.Database;
using WebStore.Services.DataProviders.CookiesDataProvider;
using WebStore.Services.DataProviders.MSSQLDataProvider;

namespace WebStore.ServiceHosting
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDBContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IEmployeeDataProvider, EmployeeDataProvider>();
            services.AddScoped<IProductDataProvider, ProductDataProvider>();
            //services.AddScoped<ICartDataProvider, CookieCartProvider>();
            services.AddScoped<IOrderDataProvider, OrderDataProvider>();
            services.AddTransient<WebStoreDataInitialize>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
