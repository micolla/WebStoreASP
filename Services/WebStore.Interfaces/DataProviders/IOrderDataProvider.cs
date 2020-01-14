using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Entity;

namespace WebStore.Interfaces.DataProviders
{
    public interface IOrderDataProvider
    {
        IEnumerable<Order> GetUserOrders(string UserName);

        Order GetOrderById(int id);

        Task<Order> CreateOrderAsync(Order OrderModel, Cart CartModel, string UserName);
    }
}
