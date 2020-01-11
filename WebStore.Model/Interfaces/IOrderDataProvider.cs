using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebStore.Model.Entity;

namespace WebStore.Model.Interfaces
{
    public interface IOrderDataProvider
    {
        IEnumerable<Order> GetUserOrders(string UserName);

        Order GetOrderById(int id);

        Task<Order> CreateOrderAsync(Order OrderModel, Cart CartModel, string UserName);
    }
}
