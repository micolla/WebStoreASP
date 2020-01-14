using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.ViewModels.Interfaces;

namespace WebStore.ViewModels
{
    public class BrandViewModel : INamedViewModel, IOrderedViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
