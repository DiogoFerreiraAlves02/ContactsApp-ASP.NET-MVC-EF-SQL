using ContactsApp.Models;

namespace ContactsApp.Helpers {
    public interface ISessionTemp {
        void CreateUserSession(User user);
        void RemoveUserSession();
        User GetUserSession();
    }
}
