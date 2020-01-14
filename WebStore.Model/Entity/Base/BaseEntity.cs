using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Model.Entity.Base.Interfaces;

namespace WebStore.Model.Entity.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => _id = value;
        }
    }
}
