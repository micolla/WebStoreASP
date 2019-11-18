using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.ViewModels;
using WebStore.Data.Interfaces;
using WebStore.Infrastructure.Mappings;

namespace WebStore.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeDataProvider _EmployeeData;
        public EmployeeController(IEmployeeDataProvider employeeDataProvider)
        {
            _EmployeeData = employeeDataProvider;
        }

        public IActionResult GetEmployees()
        {
            return View(_EmployeeData.GetAll().Select(e=>e.MapEmployeeView()));
        }
        public IActionResult GetEmployeeDetails(int? id)
        {
            if (!id.HasValue)
                return View("Page404");
            var employee = _EmployeeData.GetById(id.Value).MapEmployeeView();
            if (employee is null)
                return NotFound();
            return View(employee);
        }
    }
}