using System.Collections.Generic;
using WebStore.Domain.Entity;

namespace WebStore.Interfaces.DataProviders
{
    public interface ICartDataProvider
    {
        IEnumerable<CartItem> GetCartItems();
        void AddToCart(int productId);
        void DecreaseFromCart(int productId);
        void DeleteFromCart(int productId);
        void ClearCart();
        Cart Cart { get; }
    }
}
