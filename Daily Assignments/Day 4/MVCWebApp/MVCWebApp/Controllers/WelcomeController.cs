using Microsoft.AspNetCore.Mvc;

namespace MVCWebApp.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult Greet()
        {
            return View();
        }
    }
}
