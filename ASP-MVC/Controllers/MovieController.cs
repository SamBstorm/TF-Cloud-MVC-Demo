using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View("_underConstruct");
        }
    }
}
