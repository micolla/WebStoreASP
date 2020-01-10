using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Model.Entity;
using WebStore.Model.Interfaces;

namespace WebStore.DAL.DataProviders.CookiesDataProvider
{
    public class CookieCartProvider : ICartDataProvider
    {
        private readonly IProductDataProvider _productDataProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        private Cart Cart 
        {
            get 
            {
                var context = _httpContextAccessor.HttpContext;
                var cookies = context.Response.Cookies;
                var cart_cookie = context.Request.Cookies[_cartName];
                if (cart_cookie is null)
                {
                    var cart = new Cart();
                    cookies.Append(_cartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                ReplaceCookie(cookies, cart_cookie);
                return JsonConvert.DeserializeObject<Cart>(cart_cookie);
            }
            set => ReplaceCookie(_httpContextAccessor.HttpContext.Response.Cookies, JsonConvert.SerializeObject(value));
        }
        private void ReplaceCookie(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(_cartName);
            cookies.Append(_cartName, cookie, new CookieOptions { Expires = DateTime.Now.AddDays(15) });
        }

        public CookieCartProvider(IProductDataProvider productDataProvider,IHttpContextAccessor httpContextAccessor)
        {
            _productDataProvider = productDataProvider;
            _httpContextAccessor = httpContextAccessor;
            var user = _httpContextAccessor.HttpContext.User;
            var user_name = user.Identity.IsAuthenticated ? user.Identity.Name : null;
            _cartName = $"cart[{user_name}]";
        }
        public void AddToCart(int productId)
        {
            var cart = Cart;
            var product=cart.Items?.FirstOrDefault(p => p.ProductId == productId);
            if (product is null)
                cart.Items.Add(new CartItem { ProductId = productId, Quantity = 1 });
            else
                product.Quantity++;
            Cart = cart;
        }

        public void ClearCart()
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        public void DecreaseFromCart(int productId)
        {
            var cart = Cart;
            var product = cart.Items.FirstOrDefault(p => p.ProductId == productId);
            if (product is null) return;
            if (product.Quantity > 1) product.Quantity--;
            if (product.Quantity < 2) cart.Items.Remove(product);
            Cart = cart;
        }

        public void DeleteFromCart(int productId)
        {
            var cart = Cart;
            var product = cart.Items.FirstOrDefault(p => p.ProductId == productId);
            if (product is null) return;
            cart.Items.Remove(product);
            Cart = cart;
        }

        public IEnumerable<CartItem> GetCartItems()
        {
            var cart = Cart;
            return cart.Items;
        }
    }
}
