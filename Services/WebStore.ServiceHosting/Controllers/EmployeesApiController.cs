using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entity;
using WebStore.Interfaces.Api;
using WebStore.Interfaces.DataProviders;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeeService
    {
        private readonly IEmployeeDataProvider _employeeDataProvider;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeDataProvider">Провайдер для работы с данными по сотрудника</param>
        public EmployeesApiController(IEmployeeDataProvider employeeDataProvider) => _employeeDataProvider = employeeDataProvider;

        [HttpPost, ActionName("Post")]
        public int Add(Employee entity) => _employeeDataProvider.Add(entity);

        [HttpGet, ActionName("Get")]
        public IEnumerable<Employee> GetAll() => _employeeDataProvider.GetAll();

        [HttpGet("{id}"), ActionName("Get")]
        public Employee GetById(int id) => _employeeDataProvider.GetById(id);

        [HttpDelete("{id}"), ActionName("Delete")]
        public bool Remove(int id) => _employeeDataProvider.Remove(id);

        [NonAction]
        public void SaveChanges() => _employeeDataProvider.SaveChanges();

        [HttpPut("{id}"), ActionName("Put")]
        public bool Update(int id, Employee entity) => _employeeDataProvider.Update(id, entity);
    }
}