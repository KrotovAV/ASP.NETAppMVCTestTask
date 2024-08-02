using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Entities;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IRepository<Contact> _contactRepo;
        //private IRepository<Category> _categoryRepo;
        //private IWebHostEnvironment _environment;
        AppDBContext _context;
        public AdminController(IRepository<Contact> contactRepo, 
            //IRepository<Category> categoryRepo,
            //IWebHostEnvironment environment,
            AppDBContext context)
        {
            _contactRepo = contactRepo;
            //_categoryRepo = categoryRepo;
            //_environment = environment;
            _context = context;
        }
        public IActionResult Index()
        {
            var users = _context.Users.ToList();

            return View(users);
        }
    }
}
