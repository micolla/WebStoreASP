using System.Collections.Generic;
using WebStore.Domain.Entity;

namespace WebStore.Interfaces.DataProviders
{
    public interface IProductDataProvider
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands();

        IEnumerable<Product> GetProducts(ProductFilter Filter = null);
        Product GetProductById(int productId);
    }
}
