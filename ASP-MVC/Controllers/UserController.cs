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

        public UserController(IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<UserListItem> model = _userRepository.Get().Select(e => e.ToListItem());
            return View(model);
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
                Guid? id = _userRepository.Login(form.ToBLL());
                if (id is null) throw new Exception("L'email et le mot de passe ne sont pas valides.");
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
                _userRepository.Insert(form.ToBLL());
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(form);
            }
        }

        public IActionResult Details(Guid id)
        {
            UserDetailsViewModel model = _userRepository.Get(id).ToDetails();
            return View(model);
        }
    }
}
