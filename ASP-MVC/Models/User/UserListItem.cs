using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models.User
{
    public class UserListItem
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }
        [DisplayName("Courriel : ")]
        public string Email { get; set; }
    }
}
