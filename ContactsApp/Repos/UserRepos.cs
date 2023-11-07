using ContactsApp.Data;
using ContactsApp.Models;
using ContactsApp.Repos.Interfaces;

namespace ContactsApp.Repos {
    public class UserRepos : IUserRepos{
        private readonly AppDbContext _dbContext;
        public UserRepos(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public User GetByLogin(string login) {
            return _dbContext.Users.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public List<User> GetAll() {
            return _dbContext.Users.ToList();
        }

        public User Create(User user) {
            user.CreateDate = DateTime.Now;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public User GetById(int id) {
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Edit(User user) {
            User userDb = GetById(user.Id);

            if (userDb == null) {
                throw new Exception("Error when updating user");
            }

            userDb.Name = user.Name;
            userDb.Login = user.Login;
            userDb.Email = user.Email;
            userDb.UpdateDate = DateTime.Now;
            userDb.Profile = user.Profile;

            _dbContext.Users.Update(userDb);
            _dbContext.SaveChanges();

            return userDb;
        }

        public bool Delete(int id) {
            User userDb = GetById(id);

            if (userDb == null) {
                throw new Exception("Error when deleting user");
            }
            _dbContext.Users.Remove(userDb);
            _dbContext.SaveChanges();
            return true;
        }

    }
}
