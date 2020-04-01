using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BattleOfRestClients.Models;
using BattleOfRestClients.Services;
using BattleOfRestClients.Interfaces;

namespace BattleOfRestClients.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRestSharpExample _restSharp;
        public HomeController(IRestSharpExample restSharp)
        {
            _restSharp = restSharp;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("RestSharp")]
        public IActionResult RestSharp()
        {
            var hero = _restSharp.GetHero("Thor");
            return View("Hero", hero);
        }
        [Route("HttpClient")]
        public IActionResult HttpClient()
        {
            var hero = new HttpClientExample().GetHero("Hulk");
            return View("Hero", hero);
        }
        [Route("WebClient")]
        public IActionResult WebClient()
        {
            var hero = new WebClientExample().GetHero("Thanos");
            return View("Hero", hero);
        }
        [Route("RestEase")]
        public IActionResult RestEase()
        {
            var hero = new RestEaseExample().GetHero("Wolverine");
            return View("Hero", hero);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
