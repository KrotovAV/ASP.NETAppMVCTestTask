using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entities;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class CategoryDetailsController : Controller
    {

        private IRepository<Category> _categoryRepo;

        public CategoryDetailsController(IRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;

        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var category = await _categoryRepo.GetAsync(id);

            return View(category);
        }
    }
}
