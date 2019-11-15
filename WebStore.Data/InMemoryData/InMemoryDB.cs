using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Entity;

namespace WebStore.Data.InMemoryData
{
    public class InMemoryDB
    {
        public List<Employee> Employees=new List<Employee>
        {
            new Employee{Id=1,FirstName="Николай",LastName="Донцов"
                ,HiringDate=DateTime.Parse("2016-05-01"),BirthDay=DateTime.Parse("1990-12-22")},
            new Employee{Id=2,FirstName="Александр",LastName="Иванов"
                ,HiringDate=DateTime.Parse("2001-05-01"),BirthDay=DateTime.Parse("1985-10-10")},
            new Employee{Id=3,FirstName="Сидор",LastName="Сидоров"
                ,HiringDate=DateTime.Parse("1999-05-01"),BirthDay=DateTime.Parse("1960-05-22")},
            new Employee{Id=4,FirstName="Инокентий",LastName="Смактуновский"
                ,HiringDate=DateTime.Parse("2019-05-01"),BirthDay=DateTime.Parse("1999-02-22")},
            new Employee{Id=5,FirstName="Афанасий",LastName="Ленин"
                ,HiringDate=DateTime.Parse("2010-05-01"),BirthDay=DateTime.Parse("1970-06-01")}
        };        
    }
}
