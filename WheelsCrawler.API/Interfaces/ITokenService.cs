using WheelsCrawler.Data.Models.Account;

namespace WheelsCrawler.API.Interfaces
{
    public interface ITokenService
    {
         string CreateToken(User user);
    }
}