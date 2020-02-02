using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using WebStore.Controllers;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Api;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CatalogControllerTests
    {
        [TestMethod]
        public void ProductDetails_Returns_Correct_View()
        {
            const int expectedId = 1;
            const decimal expectedPrice = 10m;
            var expectedName = $"Name of product {expectedId}";
            var expectedBrandName = $"Brand of product {expectedName}";

            var ProductService = new Mock<IProductService>();
            ProductService
                .Setup(service => service.GetProductById(It.IsAny<int>()))
                .Returns<int>(id => new ProductDTO
                {
                    Id = id,
                    Brand = new BrandDTO
                    {
                        Id = 1,
                        Name = $"Brand of product Name of product { id }"
                    },
                    Name = $"Name of product {id}",
                    Price = 10m,
                    ImageUrl = $"Image_id_{id}.png",
                    Order = 1
                });

            var controller = new CatalogController(ProductService.Object);
            var result = controller.ProductDetails(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var model = (result as ViewResult).Model;
            Assert.IsInstanceOfType(model, typeof(ProductViewModel));
            var modelView = model as ProductViewModel;
            Assert.AreEqual<int>(expectedId, modelView.Id);
            Assert.AreEqual<decimal>(expectedPrice, modelView.Price);
            Assert.AreEqual<string>(expectedName, modelView.Name);
            Assert.AreEqual<string>(expectedBrandName, modelView.Brand);
            ProductService.Verify(service => service.GetProductById(It.IsAny<int>()));
            ProductService.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void Details_Returns_NotFound_if_Product_not_Exists()
        {
            var product_data_mock = new Mock<IProductService>();

            product_data_mock
               .Setup(p => p.GetProductById(It.IsAny<int>()))
               .Returns(default(ProductDTO));

            var controller = new CatalogController(product_data_mock.Object);

            var result = controller.ProductDetails(1);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Shop_Returns_Correct_View()
        {
            const int expectedProductsCount = 2;
            const string expectedBrandName = "Brand 1";
            const int expectedBrandId = 1;
            const int expectedSectionId = 5;

            var ProductService = new Mock<IProductService>();
            ProductService
                .Setup(service => service.GetProducts(It.IsAny<ProductFilter>()))
                .Returns<ProductFilter>(filter => new[]
                {
                    new ProductDTO
                    {
                    Id = 1,
                    Brand = new BrandDTO
                    {
                        Id = 1,
                        Name = $"Brand 1"
                    },
                    Name = $"Name of product 1",
                    Price = 10m,
                    ImageUrl = $"Image_id_1.png",
                    Order = 1
                    },
                    new ProductDTO
                    {
                    Id = 2,
                    Brand = new BrandDTO
                    {
                        Id = 1,
                        Name = $"Brand 1"
                    },
                    Name = $"Name of product 2",
                    Price = 10m,
                    ImageUrl = $"Image_id_2.png",
                    Order = 1
                    }
                });

            var controller = new CatalogController(ProductService.Object);

            var result = controller.Shop(5,1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var model = (result as ViewResult).Model;
            Assert.IsInstanceOfType(model, typeof(CatalogViewModel));
            var modelView = model as CatalogViewModel;
            Assert.AreEqual<int>(expectedProductsCount,
                modelView.Products.Count(),
                "Checking ProductCOunt");
            Assert.AreEqual<int>(expectedProductsCount,
                modelView.Products.Where(p=>String.Equals(p.Brand,expectedBrandName)).Count(),
                "Checking BrandsName");
            Assert.AreEqual<int>(expectedSectionId,
                modelView.SectionId.Value,
                "Checking SectionId");
            Assert.AreEqual<int>(expectedBrandId,
                modelView.BrandId.Value,
                "Checking BrandId");
            ProductService.Verify(service => service.GetProducts(It.IsAny<ProductFilter>()));
            ProductService.VerifyNoOtherCalls();
        }
    }
}
