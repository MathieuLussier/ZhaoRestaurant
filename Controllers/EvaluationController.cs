using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace tp1_restaurant.Controllers
{
    public class EvaluationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
