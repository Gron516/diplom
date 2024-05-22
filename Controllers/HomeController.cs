using Microsoft.AspNetCore.Mvc;
using RecipeService.Services;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
        [HttpPost]
        public async Task<IActionResult> Index([MinLength(3)] string name)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Name = "Значение слишком короткое. Должно быть больше 3х символов ";
                return View();
            }

                if (string.IsNullOrWhiteSpace(name))
            {
                ViewBag.Name = "Вы забыли ввести название";
                return View();
            }
            else
            {
                var result = await CheckService.GetRecipe(name);
                ViewBag.Name = result;
                return View();
            }
        }
        [HttpPost("recipe")]
        public async Task<string> GetRecipe([MinLength(3)] string name)
        {
            if (!ModelState.IsValid)
            {
                return "Значение слишком короткое. Должно быть больше 3х символов ";
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return "Передайте название";
            }
            else
            {
                var result = await CheckService.GetRecipe(name);
                return result;
            }
        }

    }

}
