using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BattleOfRestClients.Interfaces;
using BattleOfRestClients.Models;
using Microsoft.Extensions.Options;
using RestEase;

namespace BattleOfRestClients.Services
{
    public class RestEaseExample : IRestEaseExample
    {
        private readonly MarvelConfig _config;
        public RestEaseExample(IOptions<MarvelConfig> config)
            => _config = config.Value;
        public Hero GetHero(string name)
        {
            var ts = DateTime.Now.Ticks.ToString();
            var hash = CreateHash(ts, _config.ApiKey, _config.PrivateKey);

            var request = RestClient.For<IRestEaseConfig>(_config.BaseUrl);
            var result = request.GetHero(name, ts, _config.ApiKey, hash, CancellationToken.None).Result;

            return result;
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