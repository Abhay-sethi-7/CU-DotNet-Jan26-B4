using GlobalMart.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMart.Controllers
{
    public class CartController : Controller
    {
        private readonly IPricingService _pricingService;
        private readonly ILogger<CartController> _logger;

        public CartController(IPricingService pricingService, ILogger<CartController> logger)
        {
            _pricingService = pricingService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Checkout(string promoCode)
        {
            var basePrice = _pricingService.GetBasePrice();
            ViewBag.Total = _pricingService.CalculatePrice(basePrice, promoCode);

            return View();
        }
    }
    
}
