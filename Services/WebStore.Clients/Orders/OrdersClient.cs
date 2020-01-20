using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain.DTO.Orders;
using WebStore.Interfaces.Api;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration config) : base(config, "api/orders") { }

        public async Task<OrderDTO> CreateOrderAsync(OrderDTO createOrderDTO, string UserName) => 
            await (await PostAsync($"{_ServiceAddress}/{UserName}", createOrderDTO))
                .Content
                .ReadAsAsync<OrderDTO>();

        public OrderDTO GetOrderById(int id) => 
            Get<OrderDTO>($"{_ServiceAddress}/{id}");

        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => 
            Get<List<OrderDTO>>($"{_ServiceAddress}/user/{UserName}");
    }
}
