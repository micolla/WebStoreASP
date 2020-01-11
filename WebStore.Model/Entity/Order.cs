using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebStore.Model.Entity.Base;
using WebStore.Model.Entity.Identity;

namespace WebStore.Model.Entity
{
    public class Order : BaseEntity
    {
        [Required]
        public virtual User User { get; set; }
        [Required,MaxLength(20,ErrorMessage ="Превышена длина")]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public Order():base()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
