using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Models {
    public class LoginModel {
        [Required(ErrorMessage = "Insert user login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Insert user password")]
        public string Password { get; set; }
    }
}
