using System;
using System.Diagnostics;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tp1_restaurant.Models;
using tp1_restaurant.Data;
using tp1_restaurant.Services;
using Microsoft.AspNetCore.Authorization;

namespace tp1_restaurant.Controllers
{
    [Route("/")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EnvReader _envReader;
        private readonly EvaluationData _evaluationData;
        private readonly EmailService _emailService;

        public HomeController(ILogger<HomeController> logger, [FromServices] EnvReader envReader, [FromServices] EvaluationData evaluationData, [FromServices] EmailService emailService)
        {
            _logger = logger;
            _envReader = envReader;
            _evaluationData = evaluationData;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (TempData["ReservationSuccess"] != null) {
                ViewBag.ReservationSuccess = TempData["ReservationSuccess"];
                TempData.Remove("ReservationSuccess");
            }
            if (TempData["contactSuccess"] != null) {
                ViewBag.ContactSuccess = TempData["contactSuccess"];
                TempData.Remove("contactSuccess");
            }
            ViewBag.evaluations = _evaluationData.GetEvaluations();
            return View();
        }

        [HttpPost]
        public IActionResult Contact(Contact contact) {
            bool isContactSent = _emailService.SendEmail(contact.Courriel, "Formulaire Contact",
            $"<h1>Nous avons bien reçu votre message de contact !</h1>" +
            $"<h3>Voici le résumé: </h3>" + 
            $"<p>{contact.Message}</p>");

            TempData["contactSuccess"] = isContactSent ? true : false;

            return Redirect("/#section-contact");
        }

        [HttpGet("DownloadMenu")]
        public IActionResult DownloadMenu() {
            return File("~/documents/ZhaoMenu.pdf", "application/pdf");
        }

        [HttpGet("DownloadVins")]
        public IActionResult DownloadVins() {
            return File("~/documents/ZhaoCarteVins.pdf", "application/pdf");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
