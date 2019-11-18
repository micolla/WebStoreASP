using System.Collections.Generic;
using WebStore.Data.Entity.Base;

namespace WebStore.Data.Interfaces
{
    public interface IDataProvider<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        int Add(T entity);
        bool Remove(T entity);
        bool Update(T entity);
        T GetById(int id);
        void SaveChanges();
    }
}
