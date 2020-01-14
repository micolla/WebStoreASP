using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class CartItemViewModel
    {
        public String Name { get; }
        public Decimal Price { get; }
        public String Image { get; }
        public int Amount { get; }
        public int Id { get; }
        public Decimal TotalPrice => Price * Amount;
        public CartItemViewModel(int productId, string name, decimal price, int amount, string image)
        {
            Name = name;
            Price = price;
            Image = image;
            Id = productId;
            Amount = amount;
        }
    }
}
