using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.DataProviders;

namespace WebStore.Components
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartDataProvider _CartService;

        public CartViewComponent(ICartDataProvider CartService) => _CartService = CartService;

        public IViewComponentResult Invoke() {
            var items = _CartService.GetCartItems();
            if (items is null) return View(new CartInfoViewModel());
            return View(new CartInfoViewModel
            {
                ProductQnty = items.Count(),
                TotalQnty = items.Sum(i => i.Quantity)
            });
        }
    }
}