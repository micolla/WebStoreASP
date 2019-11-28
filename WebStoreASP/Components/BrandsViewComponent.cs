using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Model.Interfaces;
using WebStore.ViewModels;
using WebStore.Infrastructure.Mappings;

namespace WebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductDataProvider _ProductData;
        public BrandsViewComponent(IProductDataProvider productData)
        {
            _ProductData = productData;
        }
        public IViewComponentResult Invoke() => View(GetBrands());

        private IEnumerable<BrandViewModel> GetBrands() =>
            _ProductData.GetBrands().Select(brand => brand.MapBrandToBrandView());

    }
}
