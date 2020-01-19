using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Interfaces.Api;
using WebStore.Domain.ViewModels;
using WebStore.Infrastructure.Mappings;

namespace WebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductService _ProductData;
        public BrandsViewComponent(IProductService productData)
        {
            _ProductData = productData;
        }
        public IViewComponentResult Invoke() => View(GetBrands());

        private IEnumerable<BrandViewModel> GetBrands() =>
            _ProductData.GetBrands().Select(brand => brand.MapBrandDTOToBrandView());

    }
}
