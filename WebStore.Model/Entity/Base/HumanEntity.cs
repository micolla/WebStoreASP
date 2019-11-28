using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebStore.Model.Entity.Base
{
    public abstract class HumanEntity : BaseEntity
    {
        private string _firstName;
        [Column(TypeName = "nvarchar(200)")]
        [Required]
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }
        private string _lastName;
        [Column(TypeName = "nvarchar(200)")]
        [Required]
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }
    }
}
