using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BattleOfRestClients.Models;
using BattleOfRestClients.Services;
using BattleOfRestClients.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BattleOfRestClients.Controllers
{
    public class HomeController : Controller
    {
        private readonly MarvelConfig _config;
        private readonly IRestSharpExample _restSharp;
        private readonly IHttpClientExample _httpClient;
        private readonly IWebClientExample _webClient;
        private readonly IRestEaseExample _restEase;
        public HomeController(IOptions<MarvelConfig> config, IRestSharpExample restSharp, IHttpClientExample httpClient, IWebClientExample webClient, IRestEaseExample restEase)
        {
            _config = config.Value;
            _webClient = webClient;
            _restSharp = restSharp;
            _httpClient = httpClient;
            _restEase = restEase;
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
            var hero =  _httpClient.GetHero("Hulk");
            return View("Hero", hero);
        }
        [Route("WebClient")]
        public IActionResult WebClient()
        {
            var hero = _webClient.GetHero("Thanos");
            return View("Hero", hero);
        }
        [Route("RestEase")]
        public IActionResult RestEase()
        {
            var hero = _restEase.GetHero("Wolverine");
            return View("Hero", hero);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
