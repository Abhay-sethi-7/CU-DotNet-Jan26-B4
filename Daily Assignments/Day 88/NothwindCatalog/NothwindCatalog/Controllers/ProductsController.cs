using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
    using System.Net.Http.Json;
    using NorthwindCatalog.Services.DTOs;

namespace NothwindCatalog.Web.Controllers
{
    
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _factory;

        public ProductsController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        // GET: Products/ByCategory/1
        public async Task<IActionResult> ByCategory(int id)
        {
            var client = _factory.CreateClient("api");

            var products = await client
                .GetFromJsonAsync<List<ProductDto>>
                ($"api/products/by-category/{id}");

            return View(products);
        }
    }
}
