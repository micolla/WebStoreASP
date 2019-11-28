using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Model.Entity;
using WebStore.Model.Entity.Identity;

namespace WebStore.DAL.SQLDBData
{
    public class WebStoreDBContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Product> Products { get; set; }

        public WebStoreDBContext(DbContextOptions<WebStoreDBContext> dbContext) : base(dbContext) { }
    }
}
