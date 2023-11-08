using ContactsApp.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers {
    [LoggedUserPage]
    public class RestrictController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
