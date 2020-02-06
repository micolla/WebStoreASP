using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entity;

namespace WebStore.Domain.Mappings
{
    public static class Mapping
    {
        public static BrandDTO MapBrandToBrandDTO(this Brand brand) => new BrandDTO { Id = brand.Id, Name = brand.Name };
        public static Brand MapBrandDTOToBrand(this BrandDTO brandDTO) => new Brand { Id = brandDTO.Id, Name = brandDTO.Name };

        public static ProductDTO MapProductToProductDTO(this Product product) => new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Brand = product.Brand.MapBrandToBrandDTO(),
            ImageUrl = product.ImageUrl,
            Order = product.Order,
            Price = product.Price
        };


        public static Product MapProductDTOToProduct(this ProductDTO productDTO) => new Product
        {
            Id = productDTO.Id,
            Name = productDTO.Name,
            Brand = productDTO.Brand.MapBrandDTOToBrand(),
            ImageUrl = productDTO.ImageUrl,
            Order = productDTO.Order,
            Price = productDTO.Price
        };

        public static Order MapOrderDTOToOrder(this OrderDTO orderDTO) => new Order
        {
            Id = orderDTO.Id,
            Address = orderDTO.Address,
            Date = orderDTO.Date,
            Phone = orderDTO.Phone,
            //OrderItems = orderDTO.OrderItems.Select(i=>i.MapOrderItemDTOToOrderItem()).ToList()
        };
        public static OrderItem MapOrderItemDTOToOrderItem(this OrderItemDTO orderItemDTO) => new OrderItem
        {
            Id = orderItemDTO.Id,
            Price = orderItemDTO.Price,
            Product = new Product { Id = orderItemDTO.ProductId, Name = orderItemDTO.ProductName },
            Quantity = orderItemDTO.Quantity
        };

        public static OrderDTO MapOrderToOrderDTO(this Order order) => new OrderDTO
        {
            Id = order.Id,
            Address = order.Address,
            Date = order.Date,
            Phone = order.Phone,
            OrderItems = order.OrderItems.Select(i => i.MapOrderItemToOrderItemDTO()).ToList()
        };
        public static OrderItemDTO MapOrderItemToOrderItemDTO(this OrderItem orderItem) => new OrderItemDTO
        {
            Id = orderItem.Id,
            Price = orderItem.Price,
            ProductId = orderItem.Product.Id,
            ProductName = orderItem.Product.Name,
            Quantity = orderItem.Quantity
        };

    }
}
