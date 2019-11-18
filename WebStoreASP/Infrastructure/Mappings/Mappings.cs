using System;
using WebStore.ViewModels;
using WebStore.Data.Entity;

namespace WebStore.Infrastructure.Mappings
{
    public static class Mappings
    {
        public static EmployeeView MapEmployeeView(this Employee employee)
        {
            return new EmployeeView { 
                Id=employee.Id
                ,FirstName=employee.FirstName
                ,LastName=employee.LastName
                ,HiringDate=employee.HiringDate
                ,BirthDay=employee.BirthDay
            };
        }
    }
}
