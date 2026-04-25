using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using UserDashboardMVC.Models;

namespace UserDashboardMVC.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger)
        {
            _httpClient = httpClientFactory.CreateClient("ProductAPI");
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                Categories = GetCategories()
            };

            try
            {
                var response = await _httpClient.GetAsync("api/products");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var products = JsonSerializer.Deserialize<List<Product>>(json, options) ?? new();

                    model.HeroProducts = products.Take(3).ToList();
                    model.NewInTech = products.Skip(0).Take(4).ToList();
                    model.Trending = products.Skip(2).Take(4).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch products from API");
            }

            return View(model);
        }

        private static List<Category> GetCategories() => new()
    {
        new() { Name = "Laptop Sleeves",    ImageUrl = "https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?w=400", Slug = "laptop-sleeves" },
        new() { Name = "Phone Cases",       ImageUrl = "https://images.unsplash.com/photo-1601593346740-925612772716?w=400", Slug = "phone-cases" },
        new() { Name = "Cable Management",  ImageUrl = "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=400", Slug = "cable-management" },
        new() { Name = "Workstation Mate",  ImageUrl = "https://images.unsplash.com/photo-1593642632559-0c6d3fc62b89?w=400", Slug = "workstation" },
    };
    }
}
