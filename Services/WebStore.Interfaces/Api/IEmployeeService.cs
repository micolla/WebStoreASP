using System.Collections.Generic;
using WebStore.Domain.Entity;

namespace WebStore.Interfaces.Api
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        int Add(Employee entity);
        bool Remove(int id);
        bool Update(int id, Employee entity);
        Employee GetById(int id);
        void SaveChanges();
    }
}
