using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Entities;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class ContactDetailsController : Controller
    {
        
        private IRepository<Contact> _contactRepo;

        public ContactDetailsController(IRepository<Contact> contactRepo)
        {
            _contactRepo = contactRepo;

        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var contact = await _contactRepo.GetAsync(id);
          
            return View(contact);
        }
    }
}
