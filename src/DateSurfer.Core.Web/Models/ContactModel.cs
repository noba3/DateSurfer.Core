using System.ComponentModel.DataAnnotations;

namespace DateSurfer.Core.Web.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "Bitte gib deinen Namen ein")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bitte gib deine E-Mail ein")]
        [EmailAddress(ErrorMessage = "Bitte gib eine gültige E-Mail ein")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bitte gib eine Nachricht ein")]
        [MinLength(10, ErrorMessage = "Die Nachricht muss mindestens 10 Zeichen lang sein")]
        public string Message { get; set; }
    }
}
