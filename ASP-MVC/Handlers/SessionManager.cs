using ASP_MVC.Models;
using System.Text.Json;

namespace ASP_MVC.Handlers
{
    public class SessionManager
    {
        private ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public UserSession? UserSession
        {
            get
            {
                return JsonSerializer.Deserialize<UserSession?>(
                    _session.GetString(nameof(UserSession)) ?? "null"
                    );
            }
            set
            {
                if( value is null ) _session.Remove(nameof(UserSession));
                else
                {
                    _session.SetString(nameof(UserSession), JsonSerializer.Serialize(value));
                }
            }
        }
    }
}
