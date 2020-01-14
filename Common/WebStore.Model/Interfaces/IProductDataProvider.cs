using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Model.Entity;

namespace WebStore.Model.Interfaces
{
    public interface IProductDataProvider
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands();

        IEnumerable<Product> GetProducts(ProductFilter Filter = null);
        Product GetProductById(int productId);
    }
}
