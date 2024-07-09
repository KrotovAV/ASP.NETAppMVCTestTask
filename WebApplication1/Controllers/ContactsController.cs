using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using WebApplication1.Entities;
using WebApplication1.Entities.Enums;
using WebApplication1.Interfaces;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ContactsController : Controller
    {
        private IRepository<Contact> _contactRepo;
        private IRepository<Category> _categoryRepo;
        private IWebHostEnvironment _environment;

        public ContactsController(IRepository<Contact> contactRepo, IRepository<Category> categoryRepo, IWebHostEnvironment environment)
        {
            _contactRepo = contactRepo;
            _categoryRepo = categoryRepo;
            _environment = environment;
        }

        public async Task<IActionResult> Index(string searchBy, string searchFor, string sortBy, int? page)
        {
            IQueryable<Contact> contacts = _contactRepo.Items;

            if (string.IsNullOrEmpty(searchBy) && searchFor != null)
            {
                contacts = contacts.Where(ser =>
                    ser.FirstName.ToLower().Contains(searchFor.ToLower())
                    || ser.LastName.ToLower().Contains(searchFor.ToLower())
                    || ser.Email.ToLower().Contains(searchFor.ToLower())
                    || ser.Mobile.ToLower().Contains(searchFor.ToLower())
                    || ser.Category.CategoryName.ToLower().Contains(searchFor.ToLower())
                    || ser.BirthDate.Value.ToString().ToLower().Contains(searchFor.ToLower())
                  );
            }

            if (searchBy == "name" && searchFor != null)
            {

                // CL = CL.Where(ser => ser.Comment!=null ?
                // ser.Comment.ToLower().Contains(searchfor.ToLower()) :
                // ser.Comment != null).ToList();

                contacts = contacts.Where(ser => ser.FirstName.ToLower().Contains(searchFor.ToLower())
                || ser.LastName != null ?
                ser.LastName.ToLower().Contains(searchFor.ToLower()) :
                ser.LastName != null
                );

                //contacts = contacts.Where(ser => ser.FirstName.ToLower().Contains(searchFor.ToLower())
                //|| ser.LastName.ToLower().Contains(searchFor.ToLower()));
            }
            if (searchBy == "email" && searchFor != null)
            {
                contacts = contacts.Where(
                    ser => ser.Email != null ?
                    ser.Email.ToLower().Contains(searchFor.ToLower()) :
                    ser.Email != null
                    );

                //contacts = contacts.Where(ser => ser.Email.ToLower().Contains(searchFor.ToLower()));
            }
            if (searchBy == "phone" && searchFor != null)
            {
                contacts = contacts.Where(ser => ser.Mobile.ToLower().Contains(searchFor.ToLower()));
            }
            if (searchBy == "birthDate" && searchFor != null)
            {
                contacts = contacts.Where(ser => ser.BirthDate != null ?
                     ser.BirthDate.Value.ToString().ToLower().Contains(searchFor.ToLower()) :
                     ser.BirthDate != null
                    );
                //contacts = contacts.Where(ser => ser.BirthDate.ToString().ToLower().Contains(searchFor.ToLower()));
            }
            if (searchBy == "category" && searchFor != null)
            {
                contacts = contacts.Where(
                    ser => ser.Category != null ?
                    ser.Category.CategoryName.ToLower().Contains(searchFor.ToLower()) :
                    ser.Category != null
                    );
                //contacts = contacts.Where(ser => ser.Category.CategoryName.ToLower().Contains(searchFor.ToLower()));
            }

            switch (sortBy)
            {
                case "firstName":
                    contacts = contacts.OrderBy(o => o.FirstName);
                    break;
                case "firstNameDesc":
                    contacts = contacts.OrderByDescending(o => o.FirstName);
                    break;
                case "lastName":
                    contacts = contacts.OrderBy(o => o.LastName);
                    break;
                case "lastNameDesc":
                    contacts = contacts.OrderByDescending(o => o.LastName);
                    break;
                default:
                    break;
            }

            var contactsList = await contacts.ToListAsync();
            return View(contactsList.ToPagedList(page ?? 1, 6));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var contact = await _contactRepo.GetAsync(id);
                if (contact != null)
                {
                    var contactViewModel = new ContactViewModel()
                    {
                        Id = contact.Id,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Email = contact.Email,
                        UnDeleteAble = contact.UnDeleteAble,
                        PriorityType = contact.PriorityType,
                        Mobile = contact.Mobile,
                        Category = contact.Category,
                        PhotoPath = contact.PhotoPath,
                        BirthDate = contact.BirthDate
                    };
                    return View(contactViewModel);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactCreateViewModel contactCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                Contact contact = new Contact
                {
                    FirstName = contactCreateViewModel.FirstName,
                    LastName = contactCreateViewModel.LastName,
                    Email = contactCreateViewModel.Email,
                    UnDeleteAble = contactCreateViewModel.UnDeleteAble,
                    PriorityType = contactCreateViewModel.PriorityType,
                    Mobile = contactCreateViewModel.Mobile,
                    CategoryId = contactCreateViewModel.CategoryId,
                    BirthDate = contactCreateViewModel.BirthDate
                };
                if (contactCreateViewModel.UploadFile != null)
                {
                    contact.PhotoPath = UploadFile(contactCreateViewModel.UploadFile, contact.FirstName, contact.LastName);
                }
                await _contactRepo.AddAsync(contact);
                return RedirectToAction("Index");

            }
            await LoadDropdownList();
            return View(contactCreateViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _contactRepo.GetAsync(id);

            var contactEditViewModel = new ContactEditViewModel
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                UnDeleteAble = contact.UnDeleteAble,
                PriorityType = contact.PriorityType,
                Mobile = contact.Mobile,
                CategoryId = contact.CategoryId,
                PhotoPath = contact.PhotoPath,
                BirthDate = contact.BirthDate
            };

            await LoadDropdownList();
            return View(contactEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactEditViewModel contactEditViewModel)
        {
            if (ModelState.IsValid)
            {

                var contactToEdit = await _contactRepo.GetAsync(contactEditViewModel.Id);

                if (contactToEdit != null)
                {
                    contactToEdit.FirstName = contactEditViewModel.FirstName;
                    contactToEdit.LastName = contactEditViewModel.LastName;
                    contactToEdit.Mobile = contactEditViewModel.Mobile;
                    contactToEdit.Email = contactEditViewModel.Email;
                    contactToEdit.PriorityType = contactEditViewModel.PriorityType;
                    contactToEdit.UnDeleteAble = contactEditViewModel.UnDeleteAble;
                    contactToEdit.CategoryId = contactEditViewModel.CategoryId;

                    if (contactEditViewModel.UploadFile != null)
                    {
                        if (contactToEdit.PhotoPath != null)
                        {
                            string exitingFile = Path.Combine(_environment.WebRootPath,"img",contactToEdit.PhotoPath);
                            System.IO.File.Delete(exitingFile);
                        }
                        contactToEdit.PhotoPath = UploadFile(contactEditViewModel.UploadFile, contactToEdit.FirstName,contactToEdit.LastName);
                    }

                    await _contactRepo.UpdateAsync(contactToEdit);
                    return RedirectToAction("Index");
                }
            }
            await LoadDropdownList();
            return View(contactEditViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _contactRepo.GetAsync(id);
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactToDelete = await _contactRepo.GetAsync(id);

            if (contactToDelete.UnDeleteAble == true)
            {
                return RedirectToAction("Warning", new { id = contactToDelete.Id });
            }

            if (contactToDelete.PhotoPath != null)
            {
                string exitingFile = Path.Combine(_environment.WebRootPath, "img", contactToDelete.PhotoPath);
                System.IO.File.Delete(exitingFile);
            }
            await _contactRepo.RemoveAsync(contactToDelete.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Warning(int id)
        {
            var contact = await _contactRepo.GetAsync(id);
            if (contact != null)
            {
                var contactViewModel = new ContactViewModel()
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    UnDeleteAble = contact.UnDeleteAble,
                    PriorityType = contact.PriorityType,
                    Mobile = contact.Mobile,
                    Category = contact.Category,
                    PhotoPath = contact.PhotoPath,
                    BirthDate = contact.BirthDate
                };
                return View(contactViewModel);
            }
            return NotFound();
        }

        //[HttpPost]
        //public async Task<IActionResult> Warning(Contact contact)
        //{
            
        //    return View("Warning");
        //}

        private string UploadFile(IFormFile formFile, string firstName, string lastName)
        {
            string uniqueFileName = $"{firstName}_{lastName}_{Guid.NewGuid().ToString()}.jpg";
            string TargetPath = Path.Combine(_environment.WebRootPath, "img", uniqueFileName);
            using (var stream = new FileStream(TargetPath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            return uniqueFileName;
        }


       

        private async Task LoadDropdownList()
        {
            var categoriesData = await _categoryRepo.Items.ToListAsync();
            ViewBag.Categories = categoriesData
                     .Select(i => new SelectListItem
                     {
                         Value = i.Id.ToString(),
                         Text = i.CategoryName
                     }).ToList();
          
            ViewBag.PriorityTypesRadio = Enum.GetValues(typeof(PriorityType)).Cast<PriorityType>().ToArray();
        }
    }
}




