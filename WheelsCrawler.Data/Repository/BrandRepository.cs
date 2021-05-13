using System.Data.Entity;
using System.Linq;
using WheelsCrawler.Data.Models;

namespace WheelsCrawler.Data.Repository
{
    public class BrandRepository : GenericRepository<CarBrand>, IBrandRepository
    {
        public BrandRepository(WheelsCrawlerDbContext dbContext) : base(dbContext)
        {
        }

        public BrandRepository() : base()
        {
        }

        public new IQueryable<CarBrand> GetAll()
        {
            return _dbContext.CarBrands.AsNoTracking().Include(x => x.CarModels).AsNoTracking();
        }

        public CarBrand GetBrandByName(string brandName)
        {
            return _dbContext.CarBrands.AsNoTracking().Include(x => x.CarModels)
                                       .Where(x => x.WheelsName == brandName)
                                       .AsNoTracking()
                                       .Single();
        }
    }
}