﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels.Interfaces
{
    public interface IOrderedViewModel : IBaseViewModel
    {
        int Order { get; set; }
    }
}
