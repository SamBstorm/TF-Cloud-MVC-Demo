using ASP_MVC.Handlers;
using ASP_MVC.Mapper;
using ASP_MVC.Models.User;
using BLL_API.Entities;
using Common_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository<User> _userRepository;
        private SessionManager _sessionManager;

        public UserController(
            IUserRepository<User> userRepository,
            SessionManager sessionManager)
        {
            _userRepository = userRepository;
            _sessionManager = sessionManager;
        }

        [ConnectedAuthorize]
        public IActionResult Index()
        {
            IEnumerable<UserListItem> model = _userRepository.Get().Select(e => e.ToListItem());
            return View(model);
        }

        [NotConnectedAuthorize]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [NotConnectedAuthorize]
        public IActionResult Login(UserLoginForm form)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception("Votre formulaire contient des erreurs.");
                //Vérifier si présent de base de données :
                //Si ce n'est pas le cas : nouvelle exception : données invalides
                Guid? id = _userRepository.Login(form.ToBLL());
                if (id is null) throw new Exception("L'email et le mot de passe ne sont pas valides.");
                _sessionManager.UserSession = new Models.UserSession() { 
                    Email = form.Email,
                    ConnectedAt = DateTime.Now 
                };
                return RedirectToAction(nameof(Index),"Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }

        }

        [NotConnectedAuthorize]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [NotConnectedAuthorize]
        public IActionResult Register(UserRegisterForm form)
        {
            try
            {
                //ValidatorHandler.UserRegisterValidator(ModelState, form);
                ModelState.UserRegisterValidator(form);
                if (!ModelState.IsValid) throw new Exception("Formulaire non-valide.");
                //Traitement en base de données
                _userRepository.Insert(form.ToBLL());
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(form);
            }
        }

        [ConnectedAuthorize(emails: new string[] { "admin@admin" })]
        public IActionResult Details(Guid id)
        {
            UserDetailsViewModel model = _userRepository.Get(id).ToDetails();
            return View(model);
        }

        [ConnectedAuthorize]
        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        [ConnectedAuthorize]
        public IActionResult Logout(string email)
        {
            _sessionManager.UserSession = null;
            return RedirectToAction(nameof(Login));
        }
    }
}
