using System.Threading.Tasks;
using BattleOfRestClients.Models;

namespace BattleOfRestClients.Interfaces
{
    public interface IHttpClientExample
    {
         Hero GetHero(string name);
    }
}