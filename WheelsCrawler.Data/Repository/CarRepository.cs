using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WheelsCrawler.Data.Models;

namespace WheelsCrawler.Data.Repository
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(WheelsCrawlerDbContext dbContext) : base(dbContext)
        {
        }
        public CarRepository() : base()
        {
        }

         public new IQueryable<Car> GetAll()
        {
            return _dbContext.Cars.AsNoTracking();
        }

        public new async Task<Car> GetById(int id)
        {
            return await _dbContext.Cars
                        .Where(x => x.Id == id)
                        .AsNoTracking()
                        .SingleAsync();
        }
    }
}