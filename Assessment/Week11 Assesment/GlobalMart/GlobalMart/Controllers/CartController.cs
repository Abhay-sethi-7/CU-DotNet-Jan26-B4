using GlobalMart.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

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
            try
            {
                decimal basePrice = 100m;
                var finalPrice = _pricingService.CalculatePrice(basePrice, promoCode);

                ViewBag.Total = finalPrice;
                ViewBag.PromoCodes = _pricingService.GetAvailablePromoCodes();

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating price in CartController.Checkout (promoCode: {PromoCode})", promoCode);
                ViewBag.Error = "An error occurred while calculating the total. Check logs for details.";
                ViewBag.Total = 0m;
                ViewBag.PromoCodes = _pricingService.GetAvailablePromoCodes();
                return View();
            }
        }
    }
}
