using System.Collections.Generic;
using System.Linq;
using WebStore.Model.Entity.Base;

namespace WebStore.Model.Entity
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
