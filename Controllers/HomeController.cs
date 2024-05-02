using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.WebSockets;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
        [HttpPost]
        public IActionResult Index(string nazvan)
        {
            var resoult = PodborService.Vibor(nazvan);
            ViewBag.Nazvan = resoult;
            return View();
        }

        
    }

}
