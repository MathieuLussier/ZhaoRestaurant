using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tp1_restaurant.Data;
using tp1_restaurant.Models;
using tp1_restaurant.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace tp1_restaurant.Controllers
{
    [Route("/Reservation")]
    public class ReservationController : Controller
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly EnvReader _envReader;
        private readonly ReservationData _reservationData;
        private readonly EmailService _emailService;
        private readonly ZhaoContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReservationController(ILogger<ReservationController> logger, [FromServices] EnvReader envReader, [FromServices] ReservationData reservationData, [FromServices] EmailService emailService, ZhaoContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _envReader = envReader;
            _reservationData = reservationData;
            _emailService = emailService;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? TypeActive)
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            ViewBag.active = TypeActive;

            if (roles.Contains("Administrateur"))
            {
                if (TypeActive != null || TypeActive != 0)
                {
                    switch (TypeActive)
                    {
                        case 0:
                            return View(await _context.Reservations.ToListAsync());
                        case 1:
                            return View(await _context.Reservations.Where(p => p.active == false).ToListAsync());
                        case 2:
                            return View(await _context.Reservations.Where(p => p.active == true).ToListAsync());
                        default:
                            return View(await _context.Reservations.ToListAsync());
                    }
                }
                else
                {
                    return View(await _context.Reservations.ToListAsync());
                }
            }
            else
            {
                return View(await _context.Reservations.Where(e => e.Courriel == user.Email).ToListAsync());
            }
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] Reservation reservation, [FromQuery] bool redirectToHome)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
            }
            if (redirectToHome)
            {
                bool isReservationSent = _emailService.SendEmail(reservation.Courriel, "Réservation Zhao Restaurant",
                $"<h1>Nous avons bien reçu votre réservation !</h1>" +
                $"<h3>Voici le résumé: </h3>" +
                $"<p>Prénom: {reservation.Prenom}</p>" +
                $"<p>Nom: {reservation.Nom}</p>" +
                $"<p>Type de réservation: {reservation.TypeReservation}</p>" +
                $"<p>Courriel: {reservation.Courriel}</p>" +
                $"<p>Date et heure de réservation: {reservation.DateHeureReservation}</p>" +
                $"<p>Numéro de téléphone: {reservation.NumeroTelephone}</p>" +
                $"<p>Nombre de personnes: {reservation.NombrePersonnes}</p>"
                );

                TempData["ReservationSuccess"] = isReservationSent ? true : false;
                return Redirect("/#section-reservation");
            }
            return RedirectToAction("Index", "Reservation");
        }

        [HttpGet("Details")]
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            if (!await isOwnerAsync(reservation))
            {
                return Unauthorized();
            }

            return View(reservation);
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            if (!await isOwnerAsync(reservation))
            {
                return Unauthorized();
            }

            return View(reservation);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit([FromForm] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var roles = await _userManager.GetRolesAsync(user);
                try
                {
                    if (!await isOwnerAsync(reservation))
                    {
                        return Unauthorized();
                    }
                    if (!roles.Contains("Administrateur"))
                        reservation.active = false;
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationId))
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
            return View(reservation);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (!await isOwnerAsync(reservation))
            {
                return Unauthorized();
            }
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }

        private async Task<bool> isOwnerAsync(Reservation reservation)
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Administrateur"))
                return true;

            if (reservation.Courriel != user.Email)
                return false;

            return true;
        }
    }
}
