﻿using ContactsApp.Data;
using ContactsApp.Helpers;
using ContactsApp.Models;
using ContactsApp.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Repos {
    public class UserRepos : IUserRepos{
        private readonly AppDbContext _dbContext;
        public UserRepos(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public User GetByLoginEmail(string login, string email) {
            return _dbContext.Users.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper() && x.Email.ToUpper() == email.ToUpper());
        }

        public User GetByLogin(string login) {
            return _dbContext.Users.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public List<User> GetAll() {
            return _dbContext.Users.Include(x => x.Contacts).ToList();
        }

        public User Create(User user) {
            user.CreateDate = DateTime.Now;
            user.SetPasswordHash();
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

        public User ChangePwd(ChangePassword changePassword) {
            User user = GetById(changePassword.Id);

            if (user == null) throw new Exception("Error changing password, user not found.");

            if(!user.ValidPassword(changePassword.CurrentPassword)) throw new Exception("Current password doesn't match.");

            if(user.ValidPassword(changePassword.NewPassword)) throw new Exception("The new password must be different from the current one.");

            user.SetNewPassword(changePassword.NewPassword);
            user.UpdateDate = DateTime.Now;

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            return user;
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
