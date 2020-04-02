using System;
using System.Security.Cryptography;
using System.Text;
using BattleOfRestClients.Interfaces;
using BattleOfRestClients.Models;
using Microsoft.Extensions.Options;
using RestSharp;

namespace BattleOfRestClients.Services
{
    public class RestSharpExample : IRestSharpExample
    {
        private readonly IRestClient _client;
        private readonly MarvelConfig _config;
        public RestSharpExample(IOptions<MarvelConfig> config)
        {
            _config = config.Value;
            _client = new RestClient(_config.BaseUrl);
        }
            
        public Hero GetHero(string name)
        {
            var ts = DateTime.Now.Ticks.ToString();
            var request = new RestRequest(_config.EndPoint);
            request.AddQueryParameter("name", name);
            request.AddQueryParameter("ts", ts);
            request.AddQueryParameter("apikey", _config.ApiKey);
            request.AddQueryParameter("hash", CreateHash(ts, _config.ApiKey, _config.PrivateKey));

            var result = _client.Execute<Hero>(request);
            if (result.IsSuccessful)
                return result.Data;
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