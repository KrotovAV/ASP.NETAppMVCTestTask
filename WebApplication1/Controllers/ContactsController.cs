using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using WebApplication1.Context;
using WebApplication1.Entities;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class ContactsController : Controller
    {
        private IRepository<Contact> _contactRepo;
        private IRepository<Category> _categoryRepo;

        public ContactsController(IRepository<Contact> contactRepo, IRepository<Category> categoryRepo)
        {
            _contactRepo = contactRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactRepo.Items.ToListAsync();
            return View(contacts);
        }

        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            var categoriesData = await _categoryRepo.Items.ToListAsync();
            ViewBag.Categories = categoriesData
                     .Select(i => new SelectListItem
                     {
                         Value = i.Id.ToString(),
                         Text = i.CategoryName
                     }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _contactRepo.AddAsync(contact);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Что-то пошло не так {ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty, $"Что-то пошло не так, недопустимая модель");

            return View(contact);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _contactRepo.GetAsync(id);

            var categoriesData = await _categoryRepo.Items.ToListAsync();
            ViewBag.Categories = categoriesData
                     .Select(i => new SelectListItem
                     {
                         Value = i.Id.ToString(),
                         Text = i.CategoryName
                     }).ToList();

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contactToEdit = await _contactRepo.GetAsync(contact.Id);

                    if (contactToEdit != null)
                    {
                        contactToEdit.FirstName = contact.FirstName;
                        contactToEdit.LastName = contact.LastName;
                        contactToEdit.Mobile = contact.Mobile;
                        contactToEdit.Email = contact.Email;
                        contactToEdit.CategoryId = contact.CategoryId;

                        await _contactRepo.UpdateAsync(contactToEdit);
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Что-то пошло не так {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Что-то пошло не так, недопустимая модель");

            return View(contact);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _contactRepo.GetAsync(id);
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Contact contact)
        {
         
                try
                {
                    var contactToDelete = await _contactRepo.GetAsync(contact.Id);

                    if (contactToDelete != null)
                    {
                        await _contactRepo.RemoveAsync(contactToDelete.Id);
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Что-то пошло не так {ex.Message}");
                }
            
            return View();
        }

    }
}



