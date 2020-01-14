using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Model.Entity;
using WebStore.Model.Interfaces;
using System.Linq;
using WebStore.DAL.SQLDBData;

namespace WebStore.DAL.DataProviders.MSSQLDataProvider
{
    public class EmployeeDataProvider : IEmployeeDataProvider
    {
        WebStoreDBContext _db;
        public EmployeeDataProvider(WebStoreDBContext db) => _db = db;

        public int Add(Employee entity)
        {
            if (_db.Employees.Contains(entity)) return entity.Id;
            _db.Employees.Add(entity);
            SaveChanges();
            return entity.Id;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _db.Employees.AsEnumerable<Employee>();
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
            Employee delentity=_db.Employees.Remove(entity).Entity;
            SaveChanges();
            return true;
        }

        public void SaveChanges() => _db.SaveChanges();

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
            SaveChanges();
            return true;
        }
    }
}
