using ASP_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP_MVC.Handlers
{
    public class ConnectedAuthorizeAttribute : CustomUserAuthorizeAttribute
    {
        private string[]? _roles;
        private string[]? _emails;

        public ConnectedAuthorizeAttribute(
            string[]? emails = null, 
            string[]? roles = null)
        {
            _roles = roles;
            _emails = emails;
        }

        public override void OnAuthorization(AuthorizationFilterContext context)
        {
            UserSession? user = GetUserSession(context);
            if(user is not null)
            {
                //traitement de l'utilisateur : vérifier les rôles ou emails
                //si ok : finir prématurément
                if (_emails is null && _roles is null) return;
                if (_emails is not null && _emails.Contains(user.Email)) return;
            }
            context.Result = new RedirectToActionResult("Login", "User", null);
        }
    }
}
