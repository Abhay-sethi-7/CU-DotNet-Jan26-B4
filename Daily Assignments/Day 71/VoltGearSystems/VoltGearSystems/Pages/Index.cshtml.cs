using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VoltGearSystems.Models;
using VoltGearSystems.Services;

namespace VoltGearSystems.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LaptopService _laptopService;

        public List<Laptop> Laptops { get; set; } = new();

        public IndexModel(LaptopService laptopService)
        {
            _laptopService = laptopService;
        }

        public async Task OnGetAsync()
        {
            Laptops = await _laptopService.GetAsync();
        }
    }
}
