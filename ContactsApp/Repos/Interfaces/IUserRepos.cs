using ContactsApp.Models;

namespace ContactsApp.Repos.Interfaces {
    public interface IUserRepos {
        User GetByLogin(string login);
        List<User> GetAll();
        User GetById(int id);
        User Create(User user);
        User Edit(User user);
        bool Delete(int id);
    }
}
