using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ZhaoContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public EvaluationController(ILogger<EvaluationController> logger, [FromServices] EnvReader envReader, [FromServices] EvaluationData evaluationData, [FromServices] EmailService emailService, ZhaoContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _envReader = envReader;
            _evaluationData = evaluationData;
            _emailService = emailService;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (TempData["EvaluationSuccess"] != null)
            {
                ViewBag.EvaluationSuccess = TempData["EvaluationSuccess"];
                TempData.Remove("EvaluationSuccess");
            }

            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Administrateur"))
            {
                return View(await _context.Evaluations.ToListAsync());
            } else
            {
                return View(await _context.Evaluations.Where(e => e.Courriel == user.Email).ToListAsync());
            }
        }

        [HttpGet("Create")]
        public IActionResult Create() {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Evaluation evaluation, [FromQuery] bool redirect) {
            if (ModelState.IsValid)
            {
                _context.Add(evaluation);
                await _context.SaveChangesAsync();
            }
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
        public async Task<IActionResult> Details(int? id) {

            if (id == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations
                .FirstOrDefaultAsync(m => m.EvaluationId == id);
            if (evaluation == null)
            {
                return NotFound();
            }

            if (!await isOwnerAsync(evaluation))
            {
                return Unauthorized();
            }

            return View(evaluation);
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }

            if (!await isOwnerAsync(evaluation))
            {
                return Unauthorized();
            }

            return View(evaluation);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Evaluation evaluation) {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!await isOwnerAsync(evaluation))
                    {
                        return Unauthorized();
                    }
                    _context.Update(evaluation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluationExists(evaluation.EvaluationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(evaluation);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int id) {
            var evaluation = await _context.Evaluations.FindAsync(id);
            if (!await isOwnerAsync(evaluation))
            {
                return Unauthorized();
            }
            _context.Evaluations.Remove(evaluation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool EvaluationExists(int id)
        {
            return _context.Evaluations.Any(e => e.EvaluationId == id);
        }

        private async Task<bool> isOwnerAsync(Evaluation evaluation)
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Administrateur"))
                return true;

            if (evaluation.Courriel != user.Email)
                return false;
            
            return true;
        }
    }
}
