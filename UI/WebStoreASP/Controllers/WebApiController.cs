using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Api;

namespace WebStore.Controllers
{
    public class WebApiController : Controller
    {
        public async Task<IActionResult> Index([FromServices] IValuesService valuesService) =>
            View(await valuesService.GetAsync());
    }
}