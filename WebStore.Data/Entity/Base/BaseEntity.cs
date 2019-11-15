using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Data.Entity.Base
{
    public abstract class BaseEntity
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => _id = value;
        }
    }
}
