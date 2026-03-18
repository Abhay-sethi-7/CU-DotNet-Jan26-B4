using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wealth_Track.Data;
using Wealth_Track.Models;
using Wealth_Track.ViewModel;
namespace Wealth_Track.Controllers
{
  
    public class InvestmentsController : Controller
    {
        private readonly PortfolioContext _context;

        public InvestmentsController(PortfolioContext context)
        {
            _context = context;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var investments = await _context.Investment.ToListAsync();
            return View(investments);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View(new InvestmentCreateViewModel());
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvestmentCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = new Investment
                {
                    TickerSymbol = vm.TickerSymbol,
                    AssetName = vm.AssetName,
                    PurchasePrice = vm.Price,
                    Quantity = vm.Quantity,
                    PurchaseDate = DateTime.Now
                };

                _context.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }
    }
}
