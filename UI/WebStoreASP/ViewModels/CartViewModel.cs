using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public int ItemsCount => Items?.Sum(item => item.Amount) ?? 0;
        public decimal TotalSum => Items?.Sum(item => item.TotalPrice) ?? 0;
    }
}
