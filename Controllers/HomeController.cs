using System;
using System.Diagnostics;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tp1_restaurant.Models;
using tp1_restaurant.Data;

namespace tp1_restaurant.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EnvReader _envReader;
        private readonly EvaluationData _evaluationData;

        public HomeController(ILogger<HomeController> logger, [FromServices] EnvReader envReader, [FromServices] EvaluationData evaluationData)
        {
            _logger = logger;
            _envReader = envReader;
            _evaluationData = evaluationData;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (TempData["ReservationSuccess"] != null) {
                ViewBag.ReservationSuccess = true;
                TempData.Remove("ReservationSuccess");
            }
            if (TempData["contactSuccess"] != null) {
                ViewBag.ContactSuccess = true;
                TempData.Remove("contactSuccess");
            }
            ViewBag.evaluations = _evaluationData.GetEvaluations();
            return View();
        }

        [HttpPost]
        public IActionResult Contact(Contact contact) {
            TempData["contactSuccess"] = true;
            return Redirect("/#section-contact");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
