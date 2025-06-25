using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AgriEnergyConnectt.Models;
using Microsoft.AspNetCore.Authorization;

namespace AgriEnergyConnectt.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> ManageUsers()
        {
            var users = _userManager.Users.ToList();
            return View(users); // You’ll create a corresponding View
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null && await _roleManager.RoleExistsAsync(role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null && !user.EmailConfirmed)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                // Optional: prevent deleting yourself
                if (user.Email == User.Identity.Name)
                {
                    TempData["Error"] = "You cannot delete your own account.";
                    return RedirectToAction("ManageUsers");
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["Success"] = $"User {user.Email} deleted.";
                }
                else
                {
                    TempData["Error"] = $"Error deleting user {user.Email}.";
                }
            }

            return RedirectToAction("ManageUsers");
        }


    }
}
