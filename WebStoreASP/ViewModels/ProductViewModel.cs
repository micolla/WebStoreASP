using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebStore.ViewModels.Interfaces;

namespace WebStore.ViewModels
{
    public class ProductViewModel : INamedViewModel, IOrderedViewModel
    {
        [HiddenInput(DisplayValue =false)]
        public int Id { get; set; }
        [Display(Name="Наименование товара")]
        public string Name { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int Order { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageUrl { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Бренд")]
        public string Brand { get; set; }
    }
}
