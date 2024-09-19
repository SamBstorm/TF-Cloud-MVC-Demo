using ASP_MVC.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;

namespace ASP_MVC.Handlers
{
    public static class ValidatorHandler
    {
        public static void UserRegisterValidator(this ModelStateDictionary modelState, UserRegisterForm form) {
            try
            {
                if (!Regex.IsMatch(form.Password, "[a-z]{1,}"))
                {
                    modelState.AddModelError(nameof(form.Password), "Le mot de passe doit contenir au minimum une minuscule.");
                }
                if (!Regex.IsMatch(form.Password, "[A-Z]{1,}"))
                {
                    modelState.AddModelError(nameof(form.Password), "Le mot de passe doit contenir au minimum une majuscule.");
                }
                if (!Regex.IsMatch(form.Password, @"\d{1,}"))
                {
                    modelState.AddModelError(nameof(form.Password), "Le mot de passe doit contenir au minimum un chiffre.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Mot de passe obligatoire.");
            }
        }
    }
}
