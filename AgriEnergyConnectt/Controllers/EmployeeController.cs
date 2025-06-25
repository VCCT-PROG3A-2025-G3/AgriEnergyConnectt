using AgriEnergyConnectt.Data;
using AgriEnergyConnectt.Models;
using AgriEnergyConnectt.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnectt.Controllers
{

    

    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public EmployeeController(
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> AllFarmers()
        {
            var allUsers = _userManager.Users.ToList();

            var farmers = new List<IdentityUser>();

            foreach (var user in allUsers)
            {
                if (await _userManager.IsInRoleAsync(user, "Farmer"))
                {
                    farmers.Add(user);
                }
            }

            return View(farmers);
        }

        public async Task<IActionResult> FarmerProfile(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var farmer = await _userManager.FindByIdAsync(id);
            if (farmer == null)
                return NotFound();

            var products = await _context.Products
                .Where(p => p.UserId == id)
                .ToListAsync();

            var viewModel = new FarmerProfileViewModel
            {
                Farmer = farmer,
                Products = products
            };

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> AllProducts(string? name, string? category, DateTime? date)
        {
            var query = _context.Products.Include(p => p.User).AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name.Contains(name));

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(p => p.Category == category);

            if (date.HasValue)
                query = query.Where(p => p.ProductionDate.Date == date.Value.Date);

            var products = await query.ToListAsync();

            var categories = await _context.Products
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            var viewModel = new AllProductsViewModel
            {
                Products = products,
                AvailableCategories = categories,
                Filters = new ProductFilters
                {
                    Name = name,
                    Category = category,
                    ProductionDate = date
                }
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddFarmer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFarmer(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "Email and Password are required.");
                return View();
            }

            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "A user with this email already exists.");
                return View();
            }

            var newUser = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Farmer");
                TempData["Success"] = "Farmer user added successfully!";
                return RedirectToAction("AllFarmers");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

    }
}
