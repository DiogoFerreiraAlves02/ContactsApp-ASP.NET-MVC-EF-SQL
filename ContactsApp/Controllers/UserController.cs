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
    }
}
