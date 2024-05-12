using Microsoft.AspNetCore.Mvc;
using RecipeService.Services;

namespace RecipeService.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
        [HttpPost]
        public async Task<IActionResult> Index(string name)
        {
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


    }

}
