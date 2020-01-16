using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entity.Base;
using WebStore.Domain.Entity.Base.Interfaces;

namespace WebStore.Domain.Entity
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public virtual Section ParentSection { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
