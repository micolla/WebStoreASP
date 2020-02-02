using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.DAL.SQLDBData;
using WebStore.Domain.Entity.Identity;
using WebStore.Interfaces.DataProviders;
using WebStore.Services.Database;
using WebStore.Services.DataProviders.CookiesDataProvider;
using WebStore.Services.DataProviders.MSSQLDataProvider;
using Microsoft.Extensions.Logging;
using WebStore.Logger;

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
            
            services.AddTransient<WebStoreDataInitialize>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WebStoreDBContext>()
                .AddDefaultTokenProviders();


            services.AddScoped<IEmployeeDataProvider, EmployeeDataProvider>();
            services.AddScoped<IProductDataProvider, ProductDataProvider>();
            services.AddScoped<ICartDataProvider, CookieCartProvider>();
            services.AddScoped<ICartStore, CartStore>();
            services.AddScoped<IOrderDataProvider, OrderDataProvider>();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "WebStore.API", Version = "v1" });
                opt.IncludeXmlComments("WebStore.ServiceHosting.xml");
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, WebStoreDataInitialize webStoreDataInitialize, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();
            webStoreDataInitialize.InitialAsync().Wait();
            webStoreDataInitialize.IdentityInitialAsync().Wait();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("Swagger/v1/swagger.json", "WebStore.API");
                opt.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
