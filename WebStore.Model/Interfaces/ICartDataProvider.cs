using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Model.Entity;

namespace WebStore.Model.Interfaces
{
    public interface ICartDataProvider
    {
        void AddToCart(int productId);
        void DecreaseFromCart(int productId);
        void DeleteFromCart(int productId);
        void ClearCart();
    }
}
