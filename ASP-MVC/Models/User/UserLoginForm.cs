using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models.User
{
    public class UserLoginForm
    {
        [Required(ErrorMessage = "Le courriel est obligatoire.")]
        [DisplayName("Courriel :")]
        [EmailAddress(ErrorMessage = "Le courriel n'est pas valide.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [DisplayName("Mot de passe :")]
        [MinLength(8,ErrorMessage ="Le mot de passe doit avoir au minimum 8 caractères.")]
        [MaxLength(64,ErrorMessage ="Le mot de passe doit avoir au maximum 64 caractères.")]
        [RegularExpression(@"^.*(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$", ErrorMessage = "Le mot de passe n'est pas valide.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
