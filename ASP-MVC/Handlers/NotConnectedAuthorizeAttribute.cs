using ASP_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP_MVC.Handlers
{
    public class NotConnectedAuthorizeAttribute : CustomUserAuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationFilterContext context)
        {
            UserSession? user = GetUserSession(context);
            if (user is null) { return; }
            context.Result = new RedirectToActionResult("Logout","User",null);
        }
    }
}
