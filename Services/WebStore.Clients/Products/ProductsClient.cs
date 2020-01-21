using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entity;
using WebStore.Interfaces.Api;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductService
    {
        public ProductsClient(IConfiguration config) : base(config, "api/products") { }

        public IEnumerable<BrandDTO> GetBrands() => Get<List<BrandDTO>>($"{_ServiceAddress}/brands");

        public ProductDTO GetProductById(int productId) => Get<ProductDTO>($"{_ServiceAddress}/{productId}");

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null) => Post(_ServiceAddress, Filter??new ProductFilter())
            .Content
            .ReadAsAsync<List<ProductDTO>>()
            .Result;

        public IEnumerable<Section> GetSections() => Get<List<Section>>($"{_ServiceAddress}/sections");
    }
}
