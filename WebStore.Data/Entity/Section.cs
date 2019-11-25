using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Entity.Base;
using WebStore.Data.Entity.Base.Interfaces;

namespace WebStore.Data.Entity
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }
    }
}
