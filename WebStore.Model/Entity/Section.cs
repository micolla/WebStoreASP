using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Model.Entity.Base;
using WebStore.Model.Entity.Base.Interfaces;

namespace WebStore.Model.Entity
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
