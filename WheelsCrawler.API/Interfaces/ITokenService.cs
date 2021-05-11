using System.Threading.Tasks;
using WheelsCrawler.Data.Models.Account;

namespace WheelsCrawler.API.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}