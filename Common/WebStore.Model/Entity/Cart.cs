using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entity.Base;

namespace WebStore.Domain.Entity
{
    public class Cart : BaseEntity
    {
        public List<CartItem> Items { get; set; }
        public int ItemsCount => Items?.Sum(i => i.Quantity) ?? 0;

        public Cart() : base()
        {
            Items = new List<CartItem>();
        }
    }
}
