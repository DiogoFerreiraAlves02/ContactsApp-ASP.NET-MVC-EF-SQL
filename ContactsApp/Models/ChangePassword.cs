using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Models {
    public class ChangePassword {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter current user password")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Enter new user password")]
        public string NewPassword { get; set;}
        [Required(ErrorMessage = "Confirm the user's new password")]
        [Compare("NewPassword", ErrorMessage = "Password doesn't match")]
        public string ConfirmNewPassword { get; set;}

    }
}
