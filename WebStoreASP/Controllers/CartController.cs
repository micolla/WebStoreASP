using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Model.Interfaces;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartDataProvider _cartDataProvider;

        public CartController(ICartDataProvider cartDataProvider)
        {
            _cartDataProvider = cartDataProvider;
        }
        public IActionResult Details() => View();

        public IActionResult AddToCart(int productId)
        {
            _cartDataProvider.AddToCart(productId);
            return RedirectToAction("Details");
        }

        public IActionResult DecrimentFromCart(int productId)
        {
            _cartDataProvider.DecreaseFromCart(productId);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            _cartDataProvider.DeleteFromCart(productId);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveAll()
        {
            _cartDataProvider.ClearCart();
            return RedirectToAction("Details");
        }

    }
}