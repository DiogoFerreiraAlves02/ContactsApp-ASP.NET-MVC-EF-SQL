using ContactsApp.Helpers;
using ContactsApp.Models;
using ContactsApp.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers {
    public class ChangePasswordController : Controller {
        private readonly IUserRepos _userRepos;
        private readonly ISessionTemp _sessionTemp;

        public ChangePasswordController(IUserRepos userRepos, ISessionTemp sessionTemp) {
            _userRepos=userRepos;
            _sessionTemp= sessionTemp;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Change(ChangePassword changePassword) {
            try {
                User userLogged = _sessionTemp.GetUserSession();
                changePassword.Id = userLogged.Id;
                if (ModelState.IsValid) {
                    _userRepos.ChangePwd(changePassword);
                    TempData["SuccessMessage"] = "Password changed successfully";
                    return View("Index", changePassword);
                }
                return View("Index", changePassword);
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Oops, error changing your password, try again. Error: {e.Message}";
                return View("Index", changePassword);
            }
        }
    }
}
