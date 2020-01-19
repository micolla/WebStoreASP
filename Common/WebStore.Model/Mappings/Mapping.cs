using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entity;

namespace WebStore.Domain.Mappings
{
    public static class Mapping
    {
        public static BrandDTO MapBrandToBrandDTO(this Brand brand) => new BrandDTO { Id = brand.Id, Name = brand.Name };
        public static Brand MapBrandDTOToBrand(this BrandDTO brandDTO) => new Brand { Id = brandDTO.Id, Name = brandDTO.Name };

        public static ProductDTO MapProductToProductDTO(this Product product) => new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Brand = product.Brand.MapBrandToBrandDTO(),
            ImageUrl = product.ImageUrl,
            Order = product.Order,
            Price = product.Price
        };


        public static Product MapProductDTOToProduct(this ProductDTO productDTO) => new Product
        {
            Id = productDTO.Id,
            Name = productDTO.Name,
            Brand = productDTO.Brand.MapBrandDTOToBrand(),
            ImageUrl = productDTO.ImageUrl,
            Order = productDTO.Order,
            Price = productDTO.Price
        };

    }
}
