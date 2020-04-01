using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BattleOfRestClients.Interfaces;
using BattleOfRestClients.Models;
using RestEase;

namespace BattleOfRestClients.Services
{
    public class RestEaseExample
    {
        public Hero GetHero(string name)
        {
            var ts = DateTime.Now.Ticks.ToString();
            var apiKey = "295fe85fa0d9c2aad016f22522177752";
            var hash = CreateHash(ts, "295fe85fa0d9c2aad016f22522177752", "1f85b14d20cad3d3253510f53514c31cafea5bdc");
            var request = RestClient.For<IRestEaseExample>("https://gateway.marvel.com:443/v1/public/");
            var result = request.GetHero(name, ts, apiKey, hash, CancellationToken.None).Result;

            return result ?? null;
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