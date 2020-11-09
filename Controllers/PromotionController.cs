using System;
using System.Diagnostics;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tp1_restaurant.Models;
using tp1_restaurant.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace tp1_restaurant.Controllers
{
    [Route("/Promotion")]
    public class PromotionController : Controller
    {
        private readonly ILogger<PromotionController> _logger;
        private readonly EnvReader _envReader;
        private readonly PromotionData _promotionData;
        private readonly ZhaoContext _context;

        public PromotionController(ILogger<PromotionController> logger, [FromServices] EnvReader envReader, [FromServices] PromotionData promotionData, ZhaoContext context)
        {
            _logger = logger;
            _envReader = envReader;
            _promotionData = promotionData;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(TypePromotion? typePromotion)
        {
            if ((typePromotion == null || !Enum.IsDefined(typeof(TypePromotion), typePromotion)) || typePromotion == TypePromotion.Tous)
            {
                return View(await _context.Promotions.ToListAsync());
            }
            return View(await _context.Promotions.Where(p => p.TypePromotion == typePromotion).ToListAsync());
        }

        [HttpGet("Create")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Create() {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Promotion promotion, [FromQuery] bool redirect) {
            if (ModelState.IsValid)
            {
                _context.Add(promotion);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Promotion");
        }

        [HttpGet("Details")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id) {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions
                .FirstOrDefaultAsync(m => m.PromotionId == id);
            if (promotion == null)
            {
                return NotFound();
            }
            return View(promotion);
        }

        [HttpGet("Edit")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }
            return View(promotion);
        }

        [HttpPost("Edit")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Edit(Promotion promotion) {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promotion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromotionExists(promotion.PromotionId))
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
            return View(promotion);
        }

        [HttpGet("Delete")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id) {
            var promotion = await _context.Promotions.FindAsync(id);
            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool PromotionExists(int id)
        {
            return _context.Promotions.Any(e => e.PromotionId == id);
        }
    }
}
