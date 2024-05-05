using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.WebSockets;
using RecipeService.Models;
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
                ViewBag.Name = "„B„ „x„p„q„„|„y „r„r„u„ƒ„„„y „~„p„x„r„p„~„y„u";
                return View();
            }
            else
            {
                var lowerCaseName = name.ToLower(); // „P„‚„u„€„q„‚„p„x„€„r„p„~„y„u „r „~„y„w„~„y„z „‚„u„s„y„ƒ„„„‚
                var result = CheckService.GetRecipe(lowerCaseName);
                ViewBag.Name = result;
                return View();
            }
        }


    }

}
