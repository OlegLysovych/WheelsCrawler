using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WheelsCrawler.Data.Helpers;
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

        public async Task<IQueryable<Car>> GetAll(UserParams userParams)
        {

            var query = _dbContext.Cars.AsNoTracking()
                                       .Include(x => x.RelatedQueryUrl)
                                       .AsNoTracking()
                                       .Include(x => x.CarBrand)
                                       .AsNoTracking()
                                       .Include(x => x.CarModel)
                                       .AsNoTracking();

            // query = query.Where(x => x.RelatedQueryUrl.UrlToScrape.ToLower() == userParams.ExactUrl.ToLower());
            return query;
            
            // return await PagedList<Car>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }
        public new IQueryable<Car> GetAll()
        {
            var query = _dbContext.Cars.AsNoTracking()
                                       .Include(x => x.RelatedQueryUrl)
                                       .AsNoTracking()
                                       .Include(x => x.CarBrand)
                                       .AsNoTracking()
                                       .Include(x => x.CarModel)
                                       .AsNoTracking();
            return query;
            
            // return await PagedList<Car>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
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