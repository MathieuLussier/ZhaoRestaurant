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

namespace tp1_restaurant.Controllers
{
    [Route("/Reservation")]
    public class ReservationController : Controller
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly EnvReader _envReader;
        private readonly ReservationData _reservationData;
        public ReservationController(ILogger<ReservationController> logger, [FromServices] EnvReader envReader, [FromServices] ReservationData reservationData)
        {
            _logger = logger;
            _envReader = envReader;
            _reservationData = reservationData;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_reservationData.GetReservations());
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromForm] Reservation reservation, [FromQuery] bool redirectToHome)
        {
            _reservationData.CreateReservation(reservation);
            if (redirectToHome)
            {
                TempData["ReservationSuccess"] = true;
                return Redirect("/#section-reservation");
            }
            return RedirectToAction("Index", "Reservation");
        }

        [HttpGet("Details")]
        
        public IActionResult Details(int id)
        {
            return View(_reservationData.GetReservationById(id));
        }

        [HttpGet("Edit")]
        public IActionResult Edit(int id)
        {
            return View(_reservationData.GetReservationById(id));
        }

        [HttpPost("Edit")]
        public IActionResult Edit([FromForm] Reservation reservation)
        {
            _reservationData.EditReservation(reservation);
            return RedirectToAction("Index", "Reservation");
        }

        [HttpGet("Delete")]
        public IActionResult Delete(int id)
        {
            _reservationData.DeleteReservationById(id);
            return RedirectToAction("Index", "Reservation");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
