using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models.User
{
    public class UserRegisterForm
    {
        [Required(ErrorMessage = "L'adresse Courriel est obligatoire.")]
        [EmailAddress(ErrorMessage = "Le format du courriel n'est pas valide.")]
        [DisplayName("Courriel : ")]
        public string Email { get; set; }

        [DisplayName("Mot de passe : ")]
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au minimum 8 caractères.")]
        [MaxLength(64, ErrorMessage = "Le mot de passe doit contenir au maximum 64 caractères.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Veuillez confirmer le mot de passe :")]
        [Required(ErrorMessage = "La confirmation du mot de passe est obligatoire.")]
        [Compare(nameof(Password), ErrorMessage = "Le mot de passe ne correspond pas.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
