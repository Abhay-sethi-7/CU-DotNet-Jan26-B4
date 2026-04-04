using Microsoft.AspNetCore.Mvc;
using TravelDestination.FrontEnd.Models;
using TravelDestination.FrontEnd.Services;
namespace TravelDestination.FrontEnd.Controllers
{
    

        public class TravelController : Controller
        {
            private readonly IDestinationService _service;

            public TravelController(IDestinationService service)
            {
                _service = service;
            }

            // GET: /Travel
            public async Task<IActionResult> Index()
            {
                var destinations = await _service.GetAllAsync();
                return View(destinations);
            }

            // GET: /Travel/Details/5
            public async Task<IActionResult> Details(int id)
            {
                var destination = await _service.GetByIdAsync(id);

                if (destination == null)
                    return NotFound();

                return View(destination);
            }

            // GET: /Travel/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: /Travel/Create
            [HttpPost]
            public async Task<IActionResult> Create(Destination destination)
            {
                if (!ModelState.IsValid)
                    return View(destination);

                await _service.CreateAsync(destination);
                return RedirectToAction(nameof(Index));
            }

            // GET: /Travel/Edit/5
            public async Task<IActionResult> Edit(int id)
            {
                var destination = await _service.GetByIdAsync(id);

                if (destination == null)
                    return NotFound();

                return View(destination);
            }

            // POST: /Travel/Edit/5
            [HttpPost]
            public async Task<IActionResult> Edit(int id, Destination destination)
            {
                if (id != destination.DestinationID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return View(destination);

                await _service.UpdateAsync(id, destination);
                return RedirectToAction(nameof(Index));
            }

            // GET: /Travel/Delete/5
            public async Task<IActionResult> Delete(int id)
            {
                var destination = await _service.GetByIdAsync(id);

                if (destination == null)
                    return NotFound();

                return View(destination);
            }

            // POST: /Travel/Delete/5
            [HttpPost, ActionName("Delete")]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
        }
    
}
