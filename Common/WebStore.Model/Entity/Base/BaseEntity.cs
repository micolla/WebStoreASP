using WebStore.Domain.Entity.Base.Interfaces;

namespace WebStore.Domain.Entity.Base
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
