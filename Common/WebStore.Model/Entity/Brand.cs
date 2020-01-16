using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entity.Base;
using WebStore.Domain.Entity.Base.Interfaces;

namespace WebStore.Domain.Entity
{
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
