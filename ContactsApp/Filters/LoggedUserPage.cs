using ContactsApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ContactsApp.Filters {
    public class LoggedUserPage : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext context) {
            string userSession = context.HttpContext.Session.GetString("LoggedUser");

            if(string.IsNullOrEmpty(userSession)) {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller", "Login"}, {"action", "Index"} });
            }
            else {
                User user = JsonConvert.DeserializeObject<User>(userSession);
                if(user == null) {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
