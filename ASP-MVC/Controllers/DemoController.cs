using ASP_MVC.Models.Demo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{
    public class DemoController : Controller
    {
        private static List<string> _list;

        public DemoController()
        {
            _list ??= new List<string>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListeMot()
        {
            return Ok(_list);
        }

        [Route("/Demo/AjouterMot/{mot}")]
        public IActionResult AjouterMot(string mot) { 
            _list.Add(mot);
            return Ok(_list);
        }

        [Route("/Demo/InitValue/{value}")]
        public IActionResult InitValue(string value)
        {
            TempData[nameof(value)] = value;
            return View();
        }

        public IActionResult ShowValue()
        {
            if (TempData.ContainsKey("value"))
            {
                TempData.Keep("value");
                return View();
            }
            return RedirectToAction(nameof(InitValue));
        }

        public IActionResult Article()
        {
            DemoViewModel model = new DemoViewModel() { 
                Title = "Cours d'ASP MVC",
                Paragraphes = new List<string>() { 
                    "Dans ce cours d'ASP MVC, nous allons découvrir l'utilisation du pattern MVC.",
                    "Le pattern MVC correspond à l'architecture Modèle-Vue-Contrôleurs, qui est représenté par convention via les dossiers homonymes.",
                    "Je vous remercie d'avoir lu."
                },
                ImgSrc = "/assets/imgs/ASP.png"
            };
            return View(model);
        }
    }
}
