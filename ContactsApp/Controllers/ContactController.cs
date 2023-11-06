using ContactsApp.Models;
using ContactsApp.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers {
    public class ContactController : Controller {
        private readonly IContactRepos _contactRepos;
        public ContactController(IContactRepos contactRepos) {
            _contactRepos = contactRepos;
        }

        public IActionResult Index() {
            List<Contact> contacts = _contactRepos.GetAll();
            return View(contacts);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contact contact) {
            try {
                if (ModelState.IsValid) {
                    _contactRepos.Create(contact);
                    TempData["SuccessMessage"] = "Contact added successfully";
                    return RedirectToAction("Index");
                }

                return View(contact);
            }
            catch(Exception e) {
                TempData["ErrorMessage"] = $"Oops, error creating contact, try again. Error: {e.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id) {
            Contact contact = _contactRepos.GetById(id);  
            return View(contact);
        }

        [HttpPost]
        public IActionResult Edit(Contact contact) {
            try {
                if (ModelState.IsValid) {
                    _contactRepos.Edit(contact);
                    TempData["SuccessMessage"] = "Contact edited successfully";
                    return RedirectToAction("Index");
                }
                return View(contact);
            }
            catch(Exception e) {
                TempData["ErrorMessage"] = $"Oops, error editing contact, try again. Error: {e.Message}";
                return RedirectToAction("Index");
            }
        }
        public IActionResult ConfirmDelete(int id) {
            Contact contact = _contactRepos.GetById(id);
            return View(contact);
        }

        public IActionResult Delete(int id) {
            try {
                bool deleted = _contactRepos.Delete(id);
                if (deleted) {
                    TempData["SuccessMessage"] = "Contact deleted successfully";
                }
                else {
                    TempData["ErrorMessage"] = $"Oops, error deleting contact, try again.";
                }
                
                return RedirectToAction("Index");
            }
            catch(Exception e) {
                TempData["ErrorMessage"] = $"Oops, error deleting contact, try again. Error: {e.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
