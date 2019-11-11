using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreASP.ViewModels
{
    public class EmployeeView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime HiringDate { get; set; }
        public int Age => (DateTime.Now - BirthDay).Days/365;
    }
}
