using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarManagemnetMVC.Controllers
{
    [ApiController]
    [Route("api/session")]
    public class SessionController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public SessionController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // Called from client on unload/visibilitychange to sign the user out.
        [HttpPost("logout-on-close")]
        [Authorize]
        public async Task<IActionResult> LogoutOnClose()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                await _signInManager.SignOutAsync();
            }

            // No content required by client; 204 is fine for sendBeacon/fetch keepalive
            return NoContent();
        }
    }
}