using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entity;
using WebStore.Domain.Mappings;
using WebStore.Interfaces.Api;
using WebStore.Interfaces.DataProviders;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderDataProvider _orderDataProvider;

        public OrdersApiController(IOrderDataProvider orderDataProvider) => _orderDataProvider = orderDataProvider;
        public async Task<OrderDTO> CreateOrderAsync(OrderDTO orderDTO, string UserName)
        {
            var order = await _orderDataProvider.CreateOrderAsync(orderDTO.MapOrderDTOToOrder(), 
                new Cart
                {
                    Items = orderDTO.OrderItems
                    .Select(i => new CartItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity
                    })
                    .ToList()
                },
                    UserName);
            return order.MapOrderToOrderDTO();
}

        public OrderDTO GetOrderById(int id) => _orderDataProvider.GetOrderById(id).MapOrderToOrderDTO();

        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => _orderDataProvider.GetUserOrders(UserName)
            .Select(o=>new OrderDTO());
    }
}