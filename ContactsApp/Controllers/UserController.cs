using ContactsApp.Models;
using ContactsApp.Repos;
using ContactsApp.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers {
    public class UserController : Controller {
        private readonly IUserRepos _userRepos;
        public UserController(IUserRepos userRepos) {
            _userRepos = userRepos;
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
    }
}
