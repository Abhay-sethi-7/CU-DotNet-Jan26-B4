using Microsoft.AspNetCore.Mvc;
using VoltGearSystems.Models;
using VoltGearSystems.Services;

namespace VoltGearSystems.Controllers
{
    public class LaptopController : Controller
    {
        private readonly LaptopService _service;

        public LaptopController(LaptopService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var laptops = await _service.GetAsync();
            return View(laptops);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Laptop laptop)
        {
            if (!ModelState.IsValid)
                return View(laptop);

            await _service.CreateAsync(laptop);

            TempData["Message"] = "Laptop successfully saved!";
            return RedirectToAction("Index");
        }
    }
}
