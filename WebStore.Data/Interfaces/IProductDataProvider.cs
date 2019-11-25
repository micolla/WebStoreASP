using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Entity;

namespace WebStore.Data.Interfaces
{
    public interface IProductDataProvider
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands();

        IEnumerable<Product> GetProducts(ProductFilter Filter = null);
    }
}
