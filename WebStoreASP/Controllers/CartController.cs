﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Model.Entity.Identity;
using WebStore.Model.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartDataProvider _cartDataProvider;
        private readonly IProductDataProvider _productDataProvider;
        private readonly UserManager<User> userManager;

        public CartController(ICartDataProvider cartDataProvider, IProductDataProvider productDataProvider, UserManager<User> userManager)
        {
            _cartDataProvider = cartDataProvider;
            _productDataProvider = productDataProvider;
            this.userManager = userManager;
        }
        public IActionResult Details() => View(new DetailsCartViewModel { CartViewModel = GetCartViewModel(), OrderViewModel = new OrderViewModel() });

        private CartViewModel GetCartViewModel()
        {
            var cartItems = _cartDataProvider.GetCartItems();
            if (cartItems == null) return new CartViewModel();
            var products = _productDataProvider.GetProducts(new Model.Entity.ProductFilter
            { Ids = cartItems.Select(i => i.ProductId) }).ToList();
            return new CartViewModel
            {
                Items = products.Select(p => new CartItemViewModel(p.Id, p.Name, p.Price, cartItems.Where(i => i.ProductId == p.Id).Select(i => i.Quantity).FirstOrDefault(), p.ImageUrl)).ToList()
            };
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

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOutAsync(OrderViewModel Model, [FromServices] IOrderDataProvider OrderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Details), new DetailsCartViewModel
                {
                    CartViewModel = GetCartViewModel(),
                    OrderViewModel = Model
                });
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var order = await OrderService.CreateOrderAsync(
                new Model.Entity.Order
                {
                    Address = Model.Address,
                    Phone = Model.Phone,
                    User = user,
                    Date = DateTime.Now
                },
                _cartDataProvider.Cart, User.Identity.Name);

            _cartDataProvider.ClearCart();

            return RedirectToAction("OrderConfirmed", new { id = order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

    }
}