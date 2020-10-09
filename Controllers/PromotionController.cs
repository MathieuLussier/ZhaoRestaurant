using System;
using System.Diagnostics;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tp1_restaurant.Models;
using tp1_restaurant.Data;

namespace tp1_restaurant.Controllers
{
    [Route("/Promotion")]
    public class PromotionController : Controller
    {
        private readonly ILogger<PromotionController> _logger;
        private readonly EnvReader _envReader;
        private readonly PromotionData _promotionData;
        public PromotionController(ILogger<PromotionController> logger, [FromServices] EnvReader envReader, [FromServices] PromotionData promotionData)
        {
            _logger = logger;
            _envReader = envReader;
            _promotionData = promotionData;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_promotionData.GetPromotions());
        }

        [HttpGet("Create")]
        public IActionResult Create() {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(Promotion promotion, [FromQuery] bool redirect) {
            _promotionData.CreatePromotion(promotion);
            return RedirectToAction("Index", "Promotion");
        }

        [HttpGet("Details")]
        public IActionResult Details(int id) {
            return View(_promotionData.GetPromotionById(id));
        }

        [HttpGet("Edit")]
        public IActionResult Edit(int id) {
            return View(_promotionData.GetPromotionById(id));
        }

        [HttpPost("Edit")]
        public IActionResult Edit(Promotion promotion) {
            _promotionData.EditPromotion(promotion);
            return RedirectToAction("Index", "Promotion");
        }

        [HttpGet("Delete")]
        public IActionResult Delete(int id) {
            _promotionData.DeletePromotionById(id);
            return RedirectToAction("Index", "Promotion");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
