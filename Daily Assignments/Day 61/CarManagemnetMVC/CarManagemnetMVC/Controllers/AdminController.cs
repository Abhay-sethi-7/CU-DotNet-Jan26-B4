using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarManagemnetMVC.Controllers
{
   

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, role);

                TempData["Success"] = $"Role '{role}' assigned to {user.Email}";
            }
            else
            {
                TempData["Error"] = "User not found!";
            }

            return RedirectToAction("Index");
        }
    }
}
