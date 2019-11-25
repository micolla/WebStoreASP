using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Model.Entity.Base;
using WebStore.Model.Entity.Base.Interfaces;

namespace WebStore.Model.Entity
{
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
