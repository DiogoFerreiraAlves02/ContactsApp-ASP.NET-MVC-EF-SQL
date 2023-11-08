using ContactsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContactsApp.ViewComponents {
    public class Menu : ViewComponent{
        public async Task<IViewComponentResult> InvokeAsync() {
            string userSession = HttpContext.Session.GetString("LoggedUser");

            if (string.IsNullOrEmpty(userSession)) return null;

            User user = JsonConvert.DeserializeObject<User>(userSession);

            return View(user);
        }
    }
}
