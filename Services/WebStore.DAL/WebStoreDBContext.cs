using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entity;
using WebStore.Domain.Entity.Identity;

namespace WebStore.DAL.SQLDBData
{
    public class WebStoreDBContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public WebStoreDBContext(DbContextOptions<WebStoreDBContext> dbContext) : base(dbContext) { }
    }
}
