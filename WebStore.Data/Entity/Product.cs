using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Entity.Base;
using WebStore.Data.Entity.Base.Interfaces;

namespace WebStore.Data.Entity
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        public int? BrandId { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}
