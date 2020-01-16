using System.Collections.Generic;
using WebStore.Domain.Entity.Base;

namespace WebStore.Interfaces.DataProviders
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
