using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entity;
using WebStore.Interfaces.Api;
using WebStore.Interfaces.DataProviders;

namespace WebStore.Services.DataProviders.CookiesDataProvider
{
    public class CookieCartProvider : ICartDataProvider
    {
        private readonly ICartStore _cartStore;        

        public CookieCartProvider(ICartStore cartStore)
        {
            _cartStore = cartStore;
        }
        public void AddToCart(int productId)
        {
            var cart = _cartStore.Cart;
            var product=cart.Items?.FirstOrDefault(p => p.ProductId == productId);
            if (product is null)
                cart.Items.Add(new CartItem { ProductId = productId, Quantity = 1 });
            else
                product.Quantity++;
            _cartStore.Cart = cart;
        }

        public void ClearCart()
        {
            var cart = _cartStore.Cart;
            cart.Items.Clear();
            _cartStore.Cart = cart;
        }

        public void DecreaseFromCart(int productId)
        {
            var cart = _cartStore.Cart;
            var product = cart.Items.FirstOrDefault(p => p.ProductId == productId);
            if (product is null) return;
            if (product.Quantity > 1) product.Quantity--;
            if (product.Quantity < 2) cart.Items.Remove(product);
            _cartStore.Cart = cart;
        }

        public void DeleteFromCart(int productId)
        {
            var cart = _cartStore.Cart;
            var product = cart.Items.FirstOrDefault(p => p.ProductId == productId);
            if (product is null) return;
            cart.Items.Remove(product);
            _cartStore.Cart = cart;
        }

        public IEnumerable<CartItem> GetCartItems()
        {
            var cart = _cartStore.Cart;
            return cart.Items;
        }
    }
}
