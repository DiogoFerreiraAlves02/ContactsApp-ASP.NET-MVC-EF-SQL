using ContactsApp.Models;

namespace ContactsApp.Repos.Interfaces {
    public interface IUserRepos {
        List<User> GetAll();
        User GetById(int id);
        User Create(User user);
        User Edit(User user);
        bool Delete(int id);
    }
}
