using ContactsApp.Filters;
using ContactsApp.Models;
using ContactsApp.Repos;
using ContactsApp.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers {
    [RestrictAdminPage]
    public class UserController : Controller {
        private readonly IUserRepos _userRepos;
        private readonly IContactRepos _contactRepos;
        public UserController(IUserRepos userRepos, IContactRepos contactRepos) {
            _userRepos = userRepos;
            _contactRepos = contactRepos;
        }
        public IActionResult Index() {
            List<User> users = _userRepos.GetAll();
            return View(users);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user) {
            try {
                if (ModelState.IsValid) {
                    _userRepos.Create(user);
                    TempData["SuccessMessage"] = "User added successfully";
                    return RedirectToAction("Index");
                }

                return View(user);
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Oops, error creating user, try again. Error: {e.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ConfirmDelete(int id) {
            User user = _userRepos.GetById(id);
            return View(user);
        }

        public IActionResult Delete(int id) {
            try {
                bool deleted = _userRepos.Delete(id);
                if (deleted) {
                    TempData["SuccessMessage"] = "User deleted successfully";
                }
                else {
                    TempData["ErrorMessage"] = $"Oops, error deleting user, try again.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Oops, error deleting user, try again. Error: {e.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id) {
            User user = _userRepos.GetById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(UserNoPassword userNoPassword) {
            try {
                User user = null;

                if (ModelState.IsValid) {
                    user = new User() {
                        Id = userNoPassword.Id,
                        Name = userNoPassword.Name,
                        Login = userNoPassword.Login,
                        Email = userNoPassword.Email,
                        Profile = userNoPassword.Profile
                    };
                    user = _userRepos.Edit(user);
                    TempData["SuccessMessage"] = "User edited successfully";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Oops, error editing user, try again. Error: {e.Message}";
                return RedirectToAction("Index");
            }
        }
        
        public IActionResult UserContactListById(int id) {
            List<Contact> contacts = _contactRepos.GetAll(id);
            return PartialView("_UserContacts", contacts);
        }
    }
}
