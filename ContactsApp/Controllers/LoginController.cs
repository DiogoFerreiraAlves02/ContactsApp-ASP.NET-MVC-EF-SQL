using ContactsApp.Models;
using ContactsApp.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers {
    public class LoginController : Controller {
        private readonly IUserRepos _userRepos;
        public LoginController(IUserRepos userRepos) {
            _userRepos= userRepos; 
        }
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Access(LoginModel loginModel) {
            try {
                if (ModelState.IsValid) {
                    User user = _userRepos.GetByLogin(loginModel.Login);

                    if(user != null) {
                        if (user.ValidPassword(loginModel.Password)) {
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["ErrorMessage"] = $"User password is wrong. Please, try again.";
                    }
                    TempData["ErrorMessage"] = $"User and/or password wrong. Please, try again.";
                }
                return View("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Oops, error logging in, try again. Error: {e.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
