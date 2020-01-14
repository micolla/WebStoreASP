using System;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels
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
