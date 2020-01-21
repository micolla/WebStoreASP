using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entity;
using WebStore.Interfaces.Api;
using WebStore.Interfaces.DataProviders;
using WebStore.Domain.Mappings;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductService
    {
        private readonly IProductDataProvider _productData;
        public ProductsApiController(IProductDataProvider productData) => _productData = productData;

        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands() => _productData.GetBrands().Select(b => b.MapBrandToBrandDTO());
        [HttpGet("{productId}"), ActionName("Get")]
        public ProductDTO GetProductById(int productId) => _productData.GetProductById(productId).MapProductToProductDTO();
        [HttpPost, ActionName("Post")]
        public IEnumerable<ProductDTO> GetProducts([FromBody] ProductFilter Filter = null) => _productData.GetProducts(Filter).Select(p => p.MapProductToProductDTO());
        [HttpGet("sections")]
        public IEnumerable<Section> GetSections() => _productData.GetSections();
    }
}