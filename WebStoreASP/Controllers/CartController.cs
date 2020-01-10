using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Model.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartDataProvider _cartDataProvider;
        private readonly IProductDataProvider _productDataProvider;

        public CartController(ICartDataProvider cartDataProvider, IProductDataProvider productDataProvider)
        {
            _cartDataProvider = cartDataProvider;
            _productDataProvider = productDataProvider;
        }
        public IActionResult Details()
        {
            var cartItems = _cartDataProvider.GetCartItems();
            if (cartItems == null) return View(new CartViewModel());
            var products = _productDataProvider.GetProducts(new Model.Entity.ProductFilter
                                    { Ids = cartItems.Select(i => i.ProductId) }).ToList();
            CartViewModel cartViewModel = new CartViewModel
                                            {
                                                Items = products.Select(p => new CartItemViewModel(p.Id, p.Name, p.Price, cartItems.Where(i=>i.ProductId==p.Id).Select(i=>i.Quantity).FirstOrDefault(), p.ImageUrl)).ToList()
                                            };
            return View(cartViewModel);
        }



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