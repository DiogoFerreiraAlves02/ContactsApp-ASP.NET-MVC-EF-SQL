﻿using ContactsApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Models {
    public class User {
        public int Id { get; set; }
        [Required(ErrorMessage = "Insert user name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Insert user login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Insert user e-mail")]
        [EmailAddress(ErrorMessage = "E-mail is not valid!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Insert user profile")]
        public ProfileEnum? Profile { get; set; }
        [Required(ErrorMessage = "Insert user password")]
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public bool ValidPassword(string password) {
            return Password == password;
        }
    }
}
