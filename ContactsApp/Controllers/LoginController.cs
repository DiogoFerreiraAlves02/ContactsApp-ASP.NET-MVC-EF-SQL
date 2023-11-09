using ContactsApp.Helpers;
using ContactsApp.Models;
using ContactsApp.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers {
    public class LoginController : Controller {
        private readonly IUserRepos _userRepos;
        private readonly ISessionTemp _sessionTemp;
        private readonly IEmail _email;
        public LoginController(IUserRepos userRepos, ISessionTemp sessionTemp, IEmail email) {
            _userRepos = userRepos;
            _sessionTemp = sessionTemp;
            _email=email;
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

        public IActionResult RedefinePassword() {
            return View();
        }

        [HttpPost]
        public IActionResult RedefinePassword(RedefinePasswordModel redefinePasswordModel) {
            try {
                if (ModelState.IsValid) {
                    User user = _userRepos.GetByLoginEmail(redefinePasswordModel.Login, redefinePasswordModel.Email);

                    if (user != null) {
                        string newPwd = user.GeneratePassword();
                        string msg = $"Your new password is: {newPwd}";

                        bool emailSended = _email.Send(user.Email, "Contacts App - New Password", msg);

                        if(emailSended) {
                            _userRepos.Edit(user);
                            TempData["SuccessMessage"] = $"A new password has been sent to your email.";
                        }
                        else {
                            TempData["ErrorMessage"] = $"Unable to send email. Please, try again.";
                        }
                      
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["ErrorMessage"] = $"Unable to reset your password. Please check the data provided.";
                }
                return View("RedefinePassword");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Oops, unable to reset your password, try again. Error: {e.Message}";
                return RedirectToAction("RedefinePassword");
            }
        }
    }
}
