using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Model.Entity;
using WebStore.Model.Interfaces;
using WebStore.DAL.SQLDBData;
using Microsoft.AspNetCore.Identity;
using WebStore.Model.Entity.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebStore.DAL.DataProviders.MSSQLDataProvider
{
    public class OrderDataProvider : IOrderDataProvider
    {
        private readonly WebStoreDBContext db;

        public OrderDataProvider(WebStoreDBContext dbContext)
        {
            this.db = dbContext;
        }
        public async Task<Order> CreateOrderAsync(Order OrderModel, Cart CartModel, string UserName)
        {

            using(var transaction = await db.Database.BeginTransactionAsync())
            {
                await db.Orders.AddAsync(OrderModel);
                foreach (var item in CartModel.Items)
                {
                    var product = db.Products.FirstOrDefault(p => p.Id == item.ProductId);
                    if (product is null)
                        throw new InvalidOperationException($"Товар с идентификатором id:{item.ProductId} отсутствует в БД");
                    var orderItem = new OrderItem { Order = OrderModel, Price = product.Price, Product = product, Quantity = item.Quantity };
                    await db.OrderItems.AddAsync(orderItem);
                }
                await db.SaveChangesAsync();
                transaction.Commit();
            }
            return OrderModel;
        }

        public Order GetOrderById(int id) => db.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.User)
                .SingleOrDefault(o => o.Id == id);

        public IEnumerable<Order> GetUserOrders(string UserName) => db.Orders
                .Include(o => o.User)
                .Include(o => o.User)
                .Where(o => o.User.UserName == UserName).ToArray();
    }
}
