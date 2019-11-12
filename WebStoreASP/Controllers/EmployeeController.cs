using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStoreASP.ViewModels;

namespace WebStore.Controllers
{
    public class EmployeeController : Controller
    {
        public static readonly List<EmployeeView> _Employees = new List<EmployeeView>
        {
            new EmployeeView{Id=1,FirstName="Николай",LastName="Донцов"
                ,HiringDate=DateTime.Parse("2016-05-01"),BirthDay=DateTime.Parse("1990-12-22")},
            new EmployeeView{Id=2,FirstName="Александр",LastName="Иванов"
                ,HiringDate=DateTime.Parse("2001-05-01"),BirthDay=DateTime.Parse("1985-10-10")},
            new EmployeeView{Id=3,FirstName="Сидор",LastName="Сидоров"
                ,HiringDate=DateTime.Parse("1999-05-01"),BirthDay=DateTime.Parse("1960-05-22")},
            new EmployeeView{Id=4,FirstName="Инокентий",LastName="Смактуновский"
                ,HiringDate=DateTime.Parse("2019-05-01"),BirthDay=DateTime.Parse("1999-02-22")},
            new EmployeeView{Id=5,FirstName="Афанасий",LastName="Ленин"
                ,HiringDate=DateTime.Parse("2010-05-01"),BirthDay=DateTime.Parse("1970-06-01")}
        };

        public IActionResult GetEmployees()
        {
            return View(_Employees);
        }
        public IActionResult GetEmployeeDetails(int id)
        {
            return View(_Employees.Where(r => r.Id == id).SingleOrDefault());
        }
    }
}