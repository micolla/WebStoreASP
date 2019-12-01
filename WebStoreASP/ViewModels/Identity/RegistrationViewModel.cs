using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels.Identity
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Подтверждение пароля")]
        [Compare(nameof(Password))]
        public string ConfirmedPassword { get; set; }
    }
}
