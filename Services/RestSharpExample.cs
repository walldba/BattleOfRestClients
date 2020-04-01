using System;
using System.Security.Cryptography;
using System.Text;
using BattleOfRestClients.Interfaces;
using BattleOfRestClients.Models;
using RestSharp;

namespace BattleOfRestClients.Services
{
    public class RestSharpExample : IRestSharpExample
    {
        private readonly IRestClient _client;
        public RestSharpExample()
            => _client = new RestClient("https://gateway.marvel.com:443/v1/public/");
        public Hero GetHero(string name)
        {
            var ts = DateTime.Now.Ticks.ToString();
            var request = new RestRequest("characters");
            request.AddQueryParameter("name", "Wolverine");
            request.AddQueryParameter("ts", ts);
            request.AddQueryParameter("apikey", "295fe85fa0d9c2aad016f22522177752");
            request.AddQueryParameter("hash", CreateHash(ts, "295fe85fa0d9c2aad016f22522177752", "1f85b14d20cad3d3253510f53514c31cafea5bdc"));

            var result = _client.Execute<Hero>(request);
            if (result.IsSuccessful)
                return result.Data;
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