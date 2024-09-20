using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP_MVC.Handlers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public abstract void OnAuthorization(AuthorizationFilterContext context);
    }
}
