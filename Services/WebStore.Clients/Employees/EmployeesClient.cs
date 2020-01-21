using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WebStore.Clients.Base;
using WebStore.Domain.Entity;
using WebStore.Interfaces.Api;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeeService
    {
        public EmployeesClient(IConfiguration config) : base(config, "api/employees") { }

        public int Add(Employee entity)
        {
            var response = Post<Employee>(_ServiceAddress, entity);
            return entity.Id;
        }

        public IEnumerable<Employee> GetAll() => Get<List<Employee>>(_ServiceAddress);

        public Employee GetById(int id) => Get<Employee>($"{_ServiceAddress}/{id}");

        public bool Remove(int id) => Delete($"{_ServiceAddress}/{id}").IsSuccessStatusCode;

        public void SaveChanges() { }

        public bool Update(int id, Employee entity) => Put<Employee>($"{_ServiceAddress}/{id}", entity).IsSuccessStatusCode;
    }
}
