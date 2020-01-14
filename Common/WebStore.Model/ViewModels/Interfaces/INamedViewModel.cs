using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.ViewModels.Interfaces
{
    public interface INamedViewModel : IBaseViewModel
    {
        string Name { get; set; }
    }
}
