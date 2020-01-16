using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.DataProviders;
using WebStore.Infrastructure.Mappings;
using Microsoft.AspNetCore.Authorization;
using WebStore.Domain.Entity.Identity;

namespace WebStore.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        IEmployeeDataProvider _EmployeeData;
        public EmployeeController(IEmployeeDataProvider employeeDataProvider)
        {
            _EmployeeData = employeeDataProvider;
        }

        private EmployeeView FindEmployee(int? id)
        {
            if (!id.HasValue)
                return null;
            var employee = _EmployeeData.GetById(id.Value)?.MapEmployeeView();
            return employee;
        }

        public IActionResult GetEmployees()
        {
            return View(_EmployeeData.GetAll().Select(e=>e.MapEmployeeView()));
        }
        public IActionResult GetEmployeeDetails(int? id)
        {
            var employee = FindEmployee(id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

        public IActionResult AddEmployee() => View(new EmployeeView());
        [HttpPost]
        public IActionResult AddEmployee(EmployeeView newEmployee)
        {
            if (!ModelState.IsValid)
                return View(newEmployee);

            newEmployee.Id = _EmployeeData.Add(newEmployee.MapEmployee());

            return RedirectToAction("GetEmployeeDetails", new { newEmployee.Id });
        }
        [Authorize(Roles = Role.Administrator)]
        public IActionResult ModifyEmployee(int? id)
        {
            var employee = FindEmployee(id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult ModifyEmployee(EmployeeView employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            if (!_EmployeeData.Update(employee.Id, employee.MapEmployee()))
                return View(employee);
            
            return RedirectToAction("GetEmployeeDetails", new { employee.Id });
        }
        [Authorize(Roles = Role.Administrator)]
        public IActionResult DeleteEmployee(int? id)
        {
            var employee = FindEmployee(id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult DeleteEmployee(EmployeeView employee)
        {
            if (!_EmployeeData.Remove(employee.Id))
                return View(employee);

            return RedirectToAction("GetEmployees");
        }

    }
}