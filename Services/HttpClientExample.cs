using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BattleOfRestClients.Interfaces;
using BattleOfRestClients.Models;

namespace BattleOfRestClients.Services
{
    public class HttpClientExample : IHttpClientExample
    {
        private readonly HttpClient _client;
        public HttpClientExample()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://gateway.marvel.com:443/v1/public/characters?");
        }

        public Hero GetHero(string name)
        {
            var ts = DateTime.Now.Ticks.ToString();
            var request = HttpUtility.ParseQueryString(string.Empty);
            request.Set("name", "Wolverine");
            request.Set("ts", ts);
            request.Set("apikey", "295fe85fa0d9c2aad016f22522177752");
            request.Set("hash", CreateHash(ts, "295fe85fa0d9c2aad016f22522177752", "1f85b14d20cad3d3253510f53514c31cafea5bdc"));

            var result = _client.GetAsync(_client.BaseAddress + request.ToString()).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadAsAsync<Hero>().Result;
            else
                return null;
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