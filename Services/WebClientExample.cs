using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;
using BattleOfRestClients.Interfaces;
using BattleOfRestClients.Models;
using Microsoft.Extensions.Options;

namespace BattleOfRestClients.Services
{
    public class WebClientExample : IWebClientExample
    {
        private readonly WebClient _client;
        private readonly MarvelConfig _config;
        public WebClientExample(IOptions<MarvelConfig> config)
        {
            _config = config.Value;
            _client = new WebClient();
            _client.BaseAddress = _config.BaseUrl;
        }
        public Hero GetHero(string name)
        {
            var ts = DateTime.Now.Ticks.ToString();
            var request = HttpUtility.ParseQueryString(string.Empty);
            request.Set("name", name);
            request.Set("ts", ts);
            request.Set("apikey", _config.ApiKey);
            request.Set("hash", CreateHash(ts, _config.ApiKey, _config.PrivateKey));

            var result = _client.DownloadString("characters?" + request.ToString());

            return JsonSerializer.Deserialize<Hero>(result);
        }

        private string CreateHash(string ts, string publicKey, string privateKey)
        {
            var bytes = Encoding.UTF8.GetBytes(ts + privateKey + publicKey);
            var gerador = MD5.Create();
            var bytesHash = gerador.ComputeHash(bytes);
            return BitConverter.ToString(bytesHash)
                .ToLower().Replace("-", String.Empty);
        }
    }
}