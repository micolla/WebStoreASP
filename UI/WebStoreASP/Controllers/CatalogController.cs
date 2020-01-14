using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Model.Entity;
using WebStore.Model.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductDataProvider _ProductData;
        public CatalogController(IProductDataProvider productData)
        {
            _ProductData = productData;
        }
        public IActionResult Shop(int? SectionId, int? BrandId)
        {
            var products = _ProductData.GetProducts(new ProductFilter { SectionId = SectionId, BrandId = BrandId });
            return View(new CatalogViewModel { 
                            BrandId=BrandId,
                            SectionId=SectionId,
                            Products= products.Select(p => new ProductViewModel
                                                            {
                                                                Id = p.Id,
                                                                Name = p.Name,
                                                                Order = p.Order,
                                                                Price = p.Price,
                                                                ImageUrl = p.ImageUrl
                                                            }).OrderBy(p => p.Order)
                            });
        }

        public IActionResult ProductDetails(int? productId)
        {
            if (!productId.HasValue)
                return NotFound();
            var product = _ProductData.GetProductById(productId.Value);
            return View(new ProductViewModel
                                {
                                    Id = product.Id,
                                    Name = product.Name,
                                    ImageUrl = product.ImageUrl,
                                    Order = product.Order,
                                    Price = product.Price,
                                    Brand=product.Brand.Name
                                });
        }
    }
}