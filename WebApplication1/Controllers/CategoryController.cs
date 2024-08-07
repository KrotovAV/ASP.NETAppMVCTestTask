﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private IRepository<Category> _categoryRepo;

        public CategoryController(IRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepo.Items.ToListAsync();
            return View(categories);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryRepo.GetAsync(id);

            return View(category);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryRepo.AddAsync(category);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Что-то пошло не так {ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty, $"Что-то пошло не так, недопустимая модель");

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepo.GetAsync(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoryToEdit = await _categoryRepo.GetAsync(category.Id);

                    if (categoryToEdit != null)
                    {
                        categoryToEdit.CategoryName = category.CategoryName;
                        await _categoryRepo.UpdateAsync(categoryToEdit);

                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Что-то пошло не так {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Что-то пошло не так, недопустимая модель");

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _categoryRepo.GetAsync(id);
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var categoryToDelete = await _categoryRepo.GetAsync(id);

            if (categoryToDelete.Contacts.Count() != 0)
            {
                return RedirectToAction("Warning", new { id = categoryToDelete.Id });
            }

            await _categoryRepo.RemoveAsync(categoryToDelete.Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Warning(int id)
        {
            var contact = await _categoryRepo.GetAsync(id);
            return View(contact);
        }
    }
}
