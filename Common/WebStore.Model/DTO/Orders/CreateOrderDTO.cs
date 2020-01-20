using System.Collections.Generic;

namespace WebStore.Domain.DTO.Orders
{
    public class CreateOrderDTO
    {
        public OrderDTO Order { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
