using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WheelsCrawler.Data.Models;

namespace WheelsCrawler.Data.Repository
{
    public class UrlRepository : GenericRepository<Url>, IUrlRepository
    {
        public UrlRepository(WheelsCrawlerDbContext dbContext) : base(dbContext)
        {
        }

        public UrlRepository() : base()
        {
        }

        public new IQueryable<Url> GetAll()
        {
            return _dbContext.Urls.AsNoTracking().Include(x => x.InterestedUsers).AsNoTracking();
        }
        public Url GetByLinkName(string link)
        {
            return _dbContext.Urls.AsNoTracking()
                        .Include(x => x.InterestedUsers).AsNoTracking()
                        .Where(x => x.UrlToScrape.ToLower() == link.ToLower())
                        .AsNoTracking()
                        .SingleOrDefault();
        }

    }
}