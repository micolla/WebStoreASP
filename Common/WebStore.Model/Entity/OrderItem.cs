using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Model.Entity.Base;

namespace WebStore.Model.Entity
{
    public class OrderItem : BaseEntity
    {
        [Required]
        public virtual Order Order { get; set; }
        [Required]
        public virtual Product Product { get; set; }

        [Required,Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}