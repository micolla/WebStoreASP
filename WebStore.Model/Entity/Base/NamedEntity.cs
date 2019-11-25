using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Model.Entity.Base
{
    public abstract class NamedEntity : BaseEntity
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value;
        }
    }
}
