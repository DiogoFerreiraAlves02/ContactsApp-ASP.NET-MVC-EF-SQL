using ContactsApp.Helpers;
using ContactsApp.Models;
using ContactsApp.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers {
    public class LoginController : Controller {
        private readonly IUserRepos _userRepos;
        private readonly ISessionTemp _sessionTemp;
        public LoginController(IUserRepos userRepos, ISessionTemp sessionTemp) {
            _userRepos = userRepos;
            _sessionTemp = sessionTemp;
        }
        public IActionResult Index() {
            //if user is already logged in redirect to home
            if(_sessionTemp.GetUserSession() != null) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Access(LoginModel loginModel) {
            try {
                if (ModelState.IsValid) {
                    User user = _userRepos.GetByLogin(loginModel.Login);

                    if(user != null) {
                        if (user.ValidPassword(loginModel.Password)) {
                            _sessionTemp.CreateUserSession(user);
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

        public IActionResult Logout() {
            _sessionTemp.RemoveUserSession();
            return RedirectToAction("Index","Login");
        }
    }
}
