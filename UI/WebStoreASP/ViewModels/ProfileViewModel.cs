using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Обязательно для заполнения")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Обязательно для заполнения")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
