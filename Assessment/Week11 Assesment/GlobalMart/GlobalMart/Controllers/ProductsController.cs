using GlobalMart.Services;
using Microsoft.AspNetCore.Mvc;
using GlobalMart.Models;
using System.Collections.Generic;

namespace GlobalMart.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IPricingService _pricingService;

        public ProductsController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        public IActionResult Index(string promoCode)
        {
            var basePrice = _pricingService.GetBasePrice();
            ViewBag.Price = _pricingService.CalculatePrice(basePrice, promoCode);
            ViewBag.PromoCodes = _pricingService.GetAvailablePromoCodes();

            return View();
        }
    }
}
