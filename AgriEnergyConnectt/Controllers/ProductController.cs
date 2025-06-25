using AgriEnergyConnectt.Data;
using AgriEnergyConnectt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnectt.Models.ViewModels;

namespace AgriEnergyConnectt.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyProducts()
        {
            var userId = _userManager.GetUserId(User);
            var products = await _context.Products
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("❌ Validation error: " + error.ErrorMessage);
                }
                return View(model);
            }

            var product = new Product
            {
                Name = model.Name,
                Category = model.Category,
                ProductionDate = model.ProductionDate,
                CreatedAt = DateTime.Now,
                UserId = _userManager.GetUserId(User)
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Product added successfully!";
            return RedirectToAction(nameof(MyProducts));
        }





    }
}
