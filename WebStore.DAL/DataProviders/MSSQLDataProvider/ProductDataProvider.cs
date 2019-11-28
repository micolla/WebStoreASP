using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.SQLDBData;
using WebStore.Model.Entity;
using WebStore.Model.Interfaces;

namespace WebStore.DAL.DataProviders.MSSQLDataProvider
{
    public class ProductDataProvider : IProductDataProvider
    {
        WebStoreDBContext _db;
        public ProductDataProvider(WebStoreDBContext db) => _db = db;
        public IEnumerable<Brand> GetBrands() => _db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products;

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            return query.AsEnumerable<Product>();
        }

        public IEnumerable<Section> GetSections() => _db.Sections;

    }
}
