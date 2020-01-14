using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebStore.Domain.Entity.Base
{
    public abstract class NamedEntity : BaseEntity
    {
        private string _name;
        [Required]
        public string Name
        {
            get => _name;
            set => _name = value;
        }
    }
}
