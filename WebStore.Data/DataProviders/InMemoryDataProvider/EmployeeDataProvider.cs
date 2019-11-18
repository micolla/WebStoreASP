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
            return _db.Employees;
        }

        public Employee GetById(int id)
        {
            var employee = _db.Employees.FirstOrDefault(e => e.Id == id);
            return employee;
        }

        public bool Remove(int id)
        {
            var entity = GetById(id);
            if (entity == null) return false;
            return _db.Employees.Remove(entity);
        }

        public void SaveChanges() { }

        public bool Update(int id, Employee entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(Employee));

            var db_employee = GetById(id);
            if (db_employee is null) return false;

            db_employee.FirstName = entity.FirstName;
            db_employee.LastName = entity.LastName;
            db_employee.HiringDate = entity.HiringDate;
            db_employee.BirthDay = entity.BirthDay;
            return true;
        }
    }
}
