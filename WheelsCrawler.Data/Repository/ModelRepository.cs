using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WheelsCrawler.Data.Models;

namespace WheelsCrawler.Data.Repository
{
    public class ModelRepository : GenericRepository<CarModel>, IModelRepository
    {
        public ModelRepository(WheelsCrawlerDbContext dbContext) : base(dbContext)
        {
        }

        public ModelRepository() : base()
        {
        }

        public new IQueryable<CarModel> GetAll()
        {
            return _dbContext.CarModels.AsNoTracking().Include(x => x.CarBrand).AsNoTracking();
        }

        public CarModel GetModelByName(string modelName)
        {
            return _dbContext.CarModels.AsNoTracking().Include(x => x.CarBrand)
                                       .Where(x => x.WheelsName == modelName)
                                       .AsNoTracking()
                                       .Single();
        }
    }
}