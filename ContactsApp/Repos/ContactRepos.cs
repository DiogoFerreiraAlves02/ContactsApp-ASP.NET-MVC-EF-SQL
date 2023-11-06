using ContactsApp.Data;
using ContactsApp.Models;
using ContactsApp.Repos.Interfaces;

namespace ContactsApp.Repos {
    public class ContactRepos : IContactRepos {
        private readonly AppDbContext _dbContext;
        public ContactRepos(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public List<Contact> GetAll() {
            return _dbContext.Contacts.ToList();
        }

        public Contact Create(Contact contact) {
            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();
            return contact;
        }

        public Contact GetById(int id) {
            return _dbContext.Contacts.FirstOrDefault(x => x.Id == id);
        }

        public Contact Edit(Contact contact) {
            Contact contactDb = GetById(contact.Id);
            
            if(contactDb == null) {
                throw new Exception("Error when updating contact");
            }

            contactDb.Name = contact.Name;
            contactDb.Email = contact.Email;
            contactDb.Phone = contact.Phone;

            _dbContext.Contacts.Update(contactDb);
            _dbContext.SaveChanges();

            return contactDb;
        }

        public bool Delete(int id) {
            Contact contactDb = GetById(id);

            if (contactDb == null) {
                throw new Exception("Error when deleting contact");
            }
            _dbContext.Contacts.Remove(contactDb);
            _dbContext.SaveChanges();
            return true;
        }

    }
}
