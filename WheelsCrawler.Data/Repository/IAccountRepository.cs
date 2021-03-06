using System.Threading.Tasks;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Models.Account;

namespace WheelsCrawler.Data.Repository
{
    public interface IAccountRepository: IGenericRepository<User>
    {
        User GetByUsername(string username);
    }
}