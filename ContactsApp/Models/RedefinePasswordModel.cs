using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Models {
    public class RedefinePasswordModel {
        [Required(ErrorMessage = "Insert login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Insert email")]
        [EmailAddress(ErrorMessage = "E-mail is not valid!")]
        public string Email { get; set; }
    }
}
