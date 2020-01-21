using System.Collections.Generic;
using WebStore.Domain.Entity;
using WebStore.Domain.DTO.Products;

namespace WebStore.Interfaces.Api
{
    public interface IProductService
    {
        IEnumerable<Section> GetSections();

        IEnumerable<BrandDTO> GetBrands();

        IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null);
        ProductDTO GetProductById(int productId);
    }
}
