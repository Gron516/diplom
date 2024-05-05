using Microsoft.AspNetCore.Mvc;
using RecipeService.Services;

namespace RecipeService.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
        [HttpPost]
        public IActionResult Index(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ViewBag.Name = "Вы забыли ввести название";
                return View();
            }
            else
            {
                var lowerCaseName = name.ToLower(); // Преобразование в нижний регистр
                var result = CheckService.GetRecipe(lowerCaseName);
                ViewBag.Name = result;
                return View();
            }
        }


    }

}
