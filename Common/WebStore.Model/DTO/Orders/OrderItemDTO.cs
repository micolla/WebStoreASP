using WebStore.Domain.Entity.Base;

namespace WebStore.Domain.DTO.Orders
{
    public class OrderItemDTO : BaseEntity
    {
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        
        public int ProductId { get; set; }
        public string ProductName { get; set; }

    }
}
