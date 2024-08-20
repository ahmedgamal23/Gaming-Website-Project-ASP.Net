using _1___Gaming_Website_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace _1___Gaming_Website_Project.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategoryData(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest("Category name cann't be empty");
            }

            var category = new Category
            {
                Name = Name 
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDeviceData(Device device)
        {
            if (device == null)
            {
                return BadRequest("Device Cann't be null");
            }

            _context.Devices.Add(device);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}
