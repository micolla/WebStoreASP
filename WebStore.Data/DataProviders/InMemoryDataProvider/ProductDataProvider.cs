using System.Collections.Generic;
using System.Linq;
using WebStore.Data.Entity;
using WebStore.Data.InMemoryData;
using WebStore.Data.Interfaces;

namespace WebStore.Data.DataProviders.InMemoryDataProvider
{
    public class ProductDataProvider : IProductDataProvider
    {
        InMemoryDB _db;
        public ProductDataProvider(InMemoryDB db)=>_db = db;
        public IEnumerable<Brand> GetBrands() => _db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            var query = _db.Products;

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            return query;
        }

        public IEnumerable<Section> GetSections() => _db.Sections;

    }
}
