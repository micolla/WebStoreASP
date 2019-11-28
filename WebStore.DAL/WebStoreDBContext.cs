using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Model.Entity;

namespace WebStore.DAL.SQLDBData
{
    public class WebStoreDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Product> Products { get; set; }

        public WebStoreDBContext(DbContextOptions<WebStoreDBContext> dbContext) : base(dbContext) { }
    }
}
