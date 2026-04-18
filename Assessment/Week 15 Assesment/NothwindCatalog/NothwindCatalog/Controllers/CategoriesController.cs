using Microsoft.AspNetCore.Mvc;
using NorthwindCatalog.Services.DTOs;
using System.Net.Http.Json;

namespace NothwindCatalog.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoriesController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("api");
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _httpClient
                .GetFromJsonAsync<List<CategoryDto>>("api/categories");

            return View(categories);
        }
    }
}