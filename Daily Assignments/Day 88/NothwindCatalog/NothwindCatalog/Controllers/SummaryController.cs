using Microsoft.AspNetCore.Mvc;
 using Microsoft.AspNetCore.Mvc;
    using System.Net.Http.Json;
    using NorthwindCatalog.Services.DTOs;

namespace NothwindCatalog.Web.Controllers
{
   
    public class SummaryController : Controller
    {
        private readonly IHttpClientFactory _factory;

        public SummaryController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _factory.CreateClient("api");

            var summary = await client
                .GetFromJsonAsync<List<CategorySummaryDto>>
                ("api/products/summary");

            return View(summary);
        }
    }
}
