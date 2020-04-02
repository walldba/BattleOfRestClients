using System;
using System.Security.Cryptography;
using System.Text;
using BattleOfRestClients.Interfaces;
using BattleOfRestClients.Models;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace BattleOfRestClients.Services
{
    public class FlurlExample : IFlurlExample
    {
        private readonly MarvelConfig _config;
        public FlurlExample(IOptions<MarvelConfig> config)
            => _config = config.Value;
        public Hero GetHero(string name)
        {
            var ts = DateTime.Now.Ticks.ToString();
            var url = _config.BaseUrl + _config.EndPoint;

            return url
                    .SetQueryParams(
                    new
                    {
                        name = name,
                        ts = ts,
                        apikey = _config.ApiKey,
                        hash = CreateHash(ts, _config.ApiKey, _config.PrivateKey)
                    })
                    .GetJsonAsync<Hero>().Result;
        }

        private string CreateHash(string ts, string publicKey, string privateKey)
        {
            var bytes = Encoding.UTF8.GetBytes(ts + privateKey + publicKey);
            var gerador = MD5.Create();
            var bytesHash = gerador.ComputeHash(bytes);
            return BitConverter.ToString(bytesHash)
                .ToLower().Replace("-", String.Empty);
        }

        //  .SetQueryParams(
        //             new
        //             {
        //                 name = name,
        //                 ts = ts,
        //                 apiKey = _config.ApiKey,
        //                 hash = CreateHash(ts, _config.ApiKey, _config.PrivateKey)
        //             })
    }
}