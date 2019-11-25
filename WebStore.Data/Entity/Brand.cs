using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Entity.Base;
using WebStore.Data.Entity.Base.Interfaces;

namespace WebStore.Data.Entity
{
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
    }
}
