using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.SQLDBData;
using WebStore.Model.Entity;
using WebStore.Model.Interfaces;

namespace WebStore.DAL.DataProviders.MSSQLDataProvider
{
    public class ProductDataProvider : IProductDataProvider
    {
        WebStoreDBContext _db;
        public ProductDataProvider(WebStoreDBContext db) => _db = db;
        public IEnumerable<Brand> GetBrands() => _db.Brands.Include(b=>b.Products).AsEnumerable();

        public Product GetProductById(int productId) =>
            _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .FirstOrDefault(p => p.Id == productId);

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products;

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            if (Filter?.Ids != null)
                query = query.Where(product =>Filter.Ids.Any(i=>i==product.Id));

            return query
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .AsEnumerable<Product>();
        }

        public IEnumerable<Section> GetSections() => 
            _db.Sections
            .Include(s=>s.Products)
            .AsEnumerable();

    }
}
