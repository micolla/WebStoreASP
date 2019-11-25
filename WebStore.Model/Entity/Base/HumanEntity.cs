using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Model.Entity.Base
{
    public abstract class HumanEntity : BaseEntity
    {
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }
    }
}
