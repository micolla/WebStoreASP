using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Components
{
    public class UserMenuTabViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() =>
            User.Identity.IsAuthenticated ? View("UserInfo") : View();
    } 
}
