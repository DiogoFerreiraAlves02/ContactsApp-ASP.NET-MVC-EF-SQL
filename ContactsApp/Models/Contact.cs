using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Models {
    public class Contact {
        public int Id { get; set; }
        [Required(ErrorMessage = "Insert contact name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Insert contact e-mail")]
        [EmailAddress(ErrorMessage = "E-mail is not valid!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Insert contact phone")]
        [Phone(ErrorMessage = "Phone is not valid!")]
        public string Phone { get; set; }

    }
}
