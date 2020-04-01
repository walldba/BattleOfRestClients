using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using BattleOfRestClients.Interfaces;
using BattleOfRestClients.Models;
using Newtonsoft.Json;

namespace BattleOfRestClients.Services
{
    public class WebClientExample : IWebClientExample
    {
        private readonly WebClient _client;
        public WebClientExample()
        {
            _client = new WebClient();
            _client.BaseAddress = "https://gateway.marvel.com:443/v1/public/";
        }
        public Hero GetHero(string name)
        {
            var ts = DateTime.Now.Ticks.ToString();
            var request = HttpUtility.ParseQueryString(string.Empty);
            request.Set("name", "Wolverine");
            request.Set("ts", ts);
            request.Set("apikey", "295fe85fa0d9c2aad016f22522177752");
            request.Set("hash", CreateHash(ts, "295fe85fa0d9c2aad016f22522177752", "1f85b14d20cad3d3253510f53514c31cafea5bdc"));

            var result = _client.DownloadString("characters?" + request.ToString());

            return JsonConvert.DeserializeObject<Hero>(result);
        }

        public string CreateHash(string ts, string publicKey, string privateKey)
        {
            var bytes = Encoding.UTF8.GetBytes(ts + privateKey + publicKey);
            var gerador = MD5.Create();
            var bytesHash = gerador.ComputeHash(bytes);
            return BitConverter.ToString(bytesHash)
                .ToLower().Replace("-", String.Empty);
        }
    }
}