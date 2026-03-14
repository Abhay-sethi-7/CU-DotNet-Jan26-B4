using Microsoft.AspNetCore.Mvc;

namespace FinTrackPro.Controllers
{
    public class MarketController : Controller
    {

        public IActionResult Summary()
        {

            ViewBag.MarketStatus = "Open";


            ViewData["TopGainer"] = "Tata";
            ViewData["Volume"] = 85000L;

            return View();
        }

        [HttpGet]
        [Route("Analyze/{ticker}/{days:int?}")]
        public IActionResult Analyze(string ticker, int? days)
        {
            int analysisDays = days ?? 30;

            ViewBag.Ticker = ticker;
            ViewBag.Days = analysisDays;

            return View();
        }
    }
}