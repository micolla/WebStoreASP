using System.Collections.Generic;
using WebStore.Data.Entity.Base;

namespace WebStore.Data.Interfaces
{
    public interface IDataProvider<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        int Add(T entity);
        bool Remove(int id);
        bool Update(int id, T entity);
        T GetById(int id);
        void SaveChanges();
    }
}
