using System.Linq;
using System.Threading.Tasks;
using WheelsCrawler.Data.Helpers;
using WheelsCrawler.Data.Models;

namespace WheelsCrawler.Data.Repository
{
    public interface ICarRepository: IGenericRepository<Car>
    {
        Task<IQueryable<Car>> GetAll(UserParams userParams);
    }
}