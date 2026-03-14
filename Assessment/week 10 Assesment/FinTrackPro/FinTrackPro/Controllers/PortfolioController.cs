using Microsoft.AspNetCore.Mvc;
using FinTrackPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinTrackPro.Controllers
{
    public class PortfolioController : Controller
    {

        private static List<Asset> assets = new List<Asset>()
        {
            new Asset { Id = 1, Name = "Jio", Value = 1500 },
            new Asset { Id = 2, Name = "Samsung", Value = 2000 },
            new Asset { Id = 3, Name = "Microsoft", Value = 1800 }
        };

        public IActionResult Index()
        {
            double total = assets.Sum(a => a.Value);

            ViewData["Total"] = total;

            return View(assets);
        }


        [Route("Asset/Info/{id}")]
        public IActionResult Details(int id)
        {
            var asset = assets.FirstOrDefault(a => a.Id == id);

            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }


        public IActionResult Delete(int id)
        {
            var asset = assets.FirstOrDefault(a => a.Id == id);

            if (asset != null)
            {
                assets.Remove(asset);

                TempData["Message"] = "Asset deleted successfully!";
            }

            return RedirectToAction("Index");
        }
    }
}