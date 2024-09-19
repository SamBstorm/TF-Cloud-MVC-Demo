using ASP_MVC.Handlers;
using ASP_MVC.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginForm form)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception("Votre formulaire contient des erreurs.");
                //Vérifier si présent de base de données :
                //Si ce n'est pas le cas : nouvelle exception : données invalides
                return RedirectToAction(nameof(Index),"Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterForm form)
        {
            try
            {
                //ValidatorHandler.UserRegisterValidator(ModelState, form);
                ModelState.UserRegisterValidator(form);
                if (!ModelState.IsValid) throw new Exception("Formulaire non-valide.");
                //Traitement en base de données
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(form);
            }
        }

        
    }
}
