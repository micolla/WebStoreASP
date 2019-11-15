using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Data.Entity.Base
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
