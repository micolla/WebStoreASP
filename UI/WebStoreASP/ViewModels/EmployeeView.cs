using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class EmployeeView
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательно для заполнения")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Обязательно для заполнения")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Обязательно для заполнения")]
        public DateTime BirthDay { get; set; }
        [Required(ErrorMessage = "Обязательно для заполнения")]
        public DateTime HiringDate { get; set; }
        public int Age => (DateTime.Now - BirthDay).Days/365;
    }
}
