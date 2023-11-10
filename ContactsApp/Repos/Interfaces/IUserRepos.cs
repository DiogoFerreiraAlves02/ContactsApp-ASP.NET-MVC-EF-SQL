using ContactsApp.Models;

namespace ContactsApp.Repos.Interfaces {
    public interface IUserRepos {
        User GetByLogin(string login);
        User GetByLoginEmail(string login, string email);
        List<User> GetAll();
        User GetById(int id);
        User Create(User user);
        User Edit(User user);
        User ChangePwd(ChangePassword changePassword);
        bool Delete(int id);
    }
}
