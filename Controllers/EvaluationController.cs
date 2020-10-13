using System;
using System.Diagnostics;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tp1_restaurant.Data;
using tp1_restaurant.Models;
using tp1_restaurant.Services;

namespace tp1_restaurant.Controllers
{
    [Route("/Evaluation")]
    public class EvaluationController : Controller
    {
        private readonly ILogger<EvaluationController> _logger;
        private readonly EnvReader _envReader;
        private readonly EvaluationData _evaluationData;
        private readonly EmailService _emailService;
        public EvaluationController(ILogger<EvaluationController> logger, [FromServices] EnvReader envReader, [FromServices] EvaluationData evaluationData, [FromServices] EmailService emailService)
        {
            _logger = logger;
            _envReader = envReader;
            _evaluationData = evaluationData;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (TempData["EvaluationSuccess"] != null)
            {
                ViewBag.EvaluationSuccess = TempData["EvaluationSuccess"];
                TempData.Remove("EvaluationSuccess");
            }
            return View(_evaluationData.GetEvaluations());
        }

        [HttpGet("Create")]
        public IActionResult Create() {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(Evaluation evaluation, [FromQuery] bool redirect) {
            _evaluationData.CreateEvaluation(evaluation);
            bool isEvaluationSent = _emailService.SendEmail(evaluation.Courriel, "Évaluation Zhao Restaurant",
                $"<h1>Nous avons bien reçu votre évaluation !</h1>" +
                $"<h3>Voici le résumé: </h3>" +
                $"<p>Prénom: {evaluation.Prenom}</p>" +
                $"<p>Nom: {evaluation.Nom}</p>" +
                $"<p>Type de réservation: {evaluation.TypeReservation}</p>" +
                $"<p>Courriel: {evaluation.Courriel}</p>" +
                $"<p>Date de la visite: {evaluation.Datevisite}</p>" +
                $"<p>Qualité du repas: {evaluation.QualiteRepas}/5</p>" +
                $"<p>Qualité du service: {evaluation.QualiteService}/5</p>" +
                $"<p>Commentaires: {evaluation.Commentaires}</p>"
                );
            TempData["EvaluationSuccess"] = isEvaluationSent ? true : false;
            return RedirectToAction("Index", "Evaluation");
        }

        [HttpGet("Details")]
        public IActionResult Details(int id) {
            return View(_evaluationData.GetEvaluationById(id));
        }

        [HttpGet("Edit")]
        public IActionResult Edit(int id) {
            return View(_evaluationData.GetEvaluationById(id));
        }

        [HttpPost("Edit")]
        public IActionResult Edit(Evaluation evaluation) {
            _evaluationData.EditEvaluation(evaluation);
            return RedirectToAction("Index", "Evaluation");
        }

        [HttpGet("Delete")]
        public IActionResult Delete(int id) {
            _evaluationData.DeleteEvaluationById(id);
            return RedirectToAction("Index", "Evaluation");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
