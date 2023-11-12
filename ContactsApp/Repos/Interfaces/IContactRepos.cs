using ContactsApp.Models;

namespace ContactsApp.Repos.Interfaces {
    public interface IContactRepos {
        List<Contact> GetAll(int userId);
        Contact GetById(int id);
        Contact Create(Contact contact);
        Contact Edit(Contact contact);
        bool Delete(int id);

    }
}
