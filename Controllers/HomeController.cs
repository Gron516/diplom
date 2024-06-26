﻿using Microsoft.AspNetCore.Mvc;
using RecipeService.Services;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
        [HttpPost]
        public async Task<IActionResult> Index([MinLength(4)] string name)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Name = "Значение слишком короткое. Должно быть больше 3х символов ";
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
        public async Task<string> GetRecipe([MinLength(4)] string name)
        {
            if (!ModelState.IsValid)
            {
                return "Значение слишком короткое. Должно быть больше 3х символов ";
            }
            else
            {
                var result = await CheckService.GetRecipe(name);
                return result;
            }
        }

    }

}
