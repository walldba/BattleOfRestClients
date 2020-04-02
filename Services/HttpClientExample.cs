using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BattleOfRestClients.Interfaces;
using BattleOfRestClients.Models;
using Microsoft.Extensions.Options;

namespace BattleOfRestClients.Services
{
    public class HttpClientExample : IHttpClientExample
    {
        private readonly HttpClient _client;
        private readonly MarvelConfig _config;
        public HttpClientExample(IOptions<MarvelConfig> config)
        {
            _config = config.Value;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_config.BaseUrl + _config.EndPoint);
        }

        public Hero GetHero(string name)
        {
            var ts = DateTime.Now.Ticks.ToString();
            var request = HttpUtility.ParseQueryString(string.Empty);
            request.Set("name", name);
            request.Set("ts", ts);
            request.Set("apikey", _config.ApiKey);
            request.Set("hash", CreateHash(ts, _config.ApiKey, _config.PrivateKey));

            var result = _client.GetAsync(_client.BaseAddress + request.ToString()).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadAsAsync<Hero>().Result;
            else
                return null;
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