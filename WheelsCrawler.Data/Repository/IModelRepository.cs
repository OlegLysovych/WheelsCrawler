using System.Threading.Tasks;
using WheelsCrawler.Data.Models;

namespace WheelsCrawler.Data.Repository
{
    public interface IModelRepository : IGenericRepository<CarModel>
    {
        CarModel GetModelByName(string modelName);
    }
}