using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entity;

namespace WebStore.Interfaces.Api
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetUserOrders(string UserName);

        OrderDTO GetOrderById(int id);

        Task<OrderDTO> CreateOrderAsync(OrderDTO createOrderDTO, string UserName);
    }
}
