using System.Threading;
using System.Threading.Tasks;
using BattleOfRestClients.Models;
using RestEase;

namespace BattleOfRestClients.Interfaces
{
    public interface IRestEaseConfig
    {
        [Get("characters?")]
        Task<Hero> GetHero(
       [Query("name")] string name, [Query("ts")]string ts, [Query("apikey")]string apikey, [Query("hash")]string hash, CancellationToken cancellationtoken);
    }
}