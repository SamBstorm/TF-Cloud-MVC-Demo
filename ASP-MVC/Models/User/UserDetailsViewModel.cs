using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models.User
{
    public class UserDetailsViewModel
    {
        [DisplayName("Courriel : ")]
        public string Email { get; set; }
        [DisplayName("Date de création : ")]
        [DataType("datetime-local")]
        public DateTime CreatedAt { get;set; }
        [DisplayName("Date de désactivation : ")]
        [DataType("datetime-local")]
        public DateTime? DisableAt { get;set; }
        [DisplayName("Nombre de jours depuis la création : ")]
        public int AccountAge { get; set; }
    }
}
