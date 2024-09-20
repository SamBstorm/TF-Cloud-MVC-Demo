using ASP_MVC.Models.User;
using BLL = BLL_API.Entities;

namespace ASP_MVC.Mapper
{
    public static class Mapper
    {
        #region User

        public static BLL.User ToBLL(this UserLoginForm form)
        {
            if(form is null) throw new ArgumentNullException(nameof(form));
            return new BLL.User(form.Email,form.Password);
        }

        public static BLL.User ToBLL(this UserRegisterForm form)
        {
            if (form is null) throw new ArgumentNullException(nameof(form));
            return new BLL.User(form.Email, form.Password);
        }

        public static UserListItem ToListItem(this BLL.User entity) 
        { 
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new UserListItem() { Id = entity.UserId, Email = entity.Email };
        }

        public static UserDetailsViewModel ToDetails(this BLL.User entity)
        {
            if(entity is null) throw new ArgumentNullException(nameof(entity));
            return new UserDetailsViewModel()
            {
                Email = entity.Email,
                CreatedAt = entity.CreatedAt,
                DisableAt = entity.DisabledAt,
                AccountAge = (int)Math.Floor(entity.AccountAge)
            };
        }
        #endregion
    }
}
