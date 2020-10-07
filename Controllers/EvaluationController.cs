using System;
using System.Diagnostics;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tp1_restaurant.Models;

namespace tp1_restaurant.Controllers
{
    public class EvaluationController : Controller
    {
        private readonly ILogger<EvaluationController> _logger;
        private readonly EnvReader _envReader;
        public EvaluationController(ILogger<EvaluationController> logger, [FromServices] EnvReader envReader)
        {
            _logger = logger;
            _envReader = envReader;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
