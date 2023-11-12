using ContactsApp.Filters;
using ContactsApp.Helpers;
using ContactsApp.Models;
using ContactsApp.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers {
    [LoggedUserPage]
    public class ContactController : Controller {
        private readonly IContactRepos _contactRepos;
        private readonly ISessionTemp _sessionTemp;
        public ContactController(IContactRepos contactRepos, ISessionTemp sessionTemp) {
            _contactRepos = contactRepos;
            _sessionTemp = sessionTemp;
        }

        public IActionResult Index() {
            User userLogged = _sessionTemp.GetUserSession();
            List<Contact> contacts = _contactRepos.GetAll(userLogged.Id);
            return View(contacts);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contact contact) {
            try {
                if (ModelState.IsValid) {
                    User userLogged = _sessionTemp.GetUserSession();
                    contact.UserId = userLogged.Id;
                    contact = _contactRepos.Create(contact);
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
                    User userLogged = _sessionTemp.GetUserSession();
                    contact.UserId = userLogged.Id;
                    contact = _contactRepos.Edit(contact);
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
