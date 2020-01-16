using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entity;
using WebStore.Interfaces.DataProviders;
using WebStore.Domain.ViewModels;

namespace WebStore.Components
{
    public class CatalogViewComponent : ViewComponent
    {
        private readonly IProductDataProvider _ProductData;
        public CatalogViewComponent(IProductDataProvider productData)
        {
            _ProductData = productData;
        }
        public IViewComponentResult Invoke(ProductFilter productFilter = null) => View(GetCatalog(productFilter));

        private CatalogViewModel GetCatalog(ProductFilter productFilter)
        {
            var products = _ProductData.GetProducts(productFilter);
            return new CatalogViewModel
            {
                BrandId = productFilter?.BrandId,
                SectionId = productFilter?.SectionId,
                Products = products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                }).OrderBy(p => p.Order)
            };
        }
    }
}
