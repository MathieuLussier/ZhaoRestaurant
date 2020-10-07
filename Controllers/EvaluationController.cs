using System;
using System.Diagnostics;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tp1_restaurant.Data;
using tp1_restaurant.Models;

namespace tp1_restaurant.Controllers
{
    [Route("/Evaluation")]
    public class EvaluationController : Controller
    {
        private readonly ILogger<EvaluationController> _logger;
        private readonly EnvReader _envReader;
        private readonly EvaluationData _evaluationData;
        public EvaluationController(ILogger<EvaluationController> logger, [FromServices] EnvReader envReader, [FromServices] EvaluationData evaluationData)
        {
            _logger = logger;
            _envReader = envReader;
            _evaluationData = evaluationData;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_evaluationData.GetEvaluations());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
