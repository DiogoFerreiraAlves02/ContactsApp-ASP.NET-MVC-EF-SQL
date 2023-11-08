using ContactsApp.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ContactsApp.Helpers {
    public class SessionTemp : ISessionTemp {
        private readonly IHttpContextAccessor _httpContext;
        public SessionTemp(IHttpContextAccessor httpContext) {
            _httpContext = httpContext;
        }
        public void CreateUserSession(User user) {
            string val = JsonConvert.SerializeObject(user);
            _httpContext.HttpContext.Session.SetString("LoggedUser",val);
        }

        public User GetUserSession() {
            string userSession = _httpContext.HttpContext.Session.GetString("LoggedUser");
            if (string.IsNullOrEmpty(userSession)) return null;
            return JsonConvert.DeserializeObject<User>(userSession);
        }

        public void RemoveUserSession() {
            _httpContext.HttpContext.Session.Remove("LoggedUser");
        }
    }
}
