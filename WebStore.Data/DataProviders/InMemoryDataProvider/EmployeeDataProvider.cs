using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Entity;
using WebStore.Data.Interfaces;
using WebStore.Data.InMemoryData;
using System.Linq;

namespace WebStore.Data.DataProviders.InMemoryDataProvider
{
    public class EmployeeDataProvider : IEmployeeDataProvider
    {
        InMemoryDB _db;
        public EmployeeDataProvider(InMemoryDB db)
        {
            _db = db;
        }
        public int Add(Employee entity)
        {
            if (_db.Employees.Contains(entity)) return entity.Id;
            entity.Id = _db.Employees.Count == 0 ? 1 : _db.Employees.Max(r => r.Id) + 1;
            _db.Employees.Add(entity);
            return entity.Id;
        }

        public IEnumerable<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public bool Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
