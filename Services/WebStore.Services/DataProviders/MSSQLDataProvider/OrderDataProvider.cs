using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entity;
using WebStore.Interfaces.DataProviders;
using WebStore.DAL.SQLDBData;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entity.Identity;

namespace WebStore.Services.DataProviders.MSSQLDataProvider
{
    public class OrderDataProvider : IOrderDataProvider
    {
        private readonly WebStoreDBContext db;
        private readonly IProductDataProvider _productDataProvider;
        private readonly UserManager<User> _UserManager;

        public OrderDataProvider(WebStoreDBContext dbContext, IProductDataProvider productDataProvider, UserManager<User> UserManager)
        {
            this.db = dbContext;
            this._productDataProvider = productDataProvider;
            _UserManager = UserManager;
        }
        public async Task<Order> CreateOrderAsync(Order OrderModel, Cart CartModel, string UserName)
        {
            var user = _UserManager.FindByNameAsync(UserName).Result;
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                OrderModel.User = user;
                await db.Orders.AddAsync(OrderModel);
                foreach (var item in CartModel.Items)
                {
                    var product = _productDataProvider.GetProductById(item.ProductId);
                    if (product is null)
                        throw new InvalidOperationException($"Товар с идентификатором id:{item.ProductId} отсутствует в БД");
                    var orderItem = new OrderItem { Order = OrderModel, Price = product.Price, Product = product, Quantity = item.Quantity };
                    await db.OrderItems.AddAsync(orderItem);
                }
                try
                {
                    await db.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                }
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
