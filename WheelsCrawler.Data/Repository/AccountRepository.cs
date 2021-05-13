using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Models.Account;

namespace WheelsCrawler.Data.Repository
{
    public class AccountRepository : GenericRepository<User>, IAccountRepository
    {
        // private readonly WheelsCrawlerDbContext _dbContext;

        public AccountRepository() : base()
        {
        }
        public AccountRepository(WheelsCrawlerDbContext dbContext) : base(dbContext)
        {
        }

        public new IQueryable<User> GetAll()
        {
            return _dbContext.Users.Include(x => x.InterestedUrls).AsNoTracking();
        }

        public new async Task<User> GetById(int id)
        {
            return _dbContext.Users.AsNoTracking()
                        .Include(x => x.InterestedUrls)
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .FirstOrDefault();
        }

        public  User GetByUsername(string username)
        {
            return  _dbContext.Users.AsNoTracking().Include(x => x.InterestedUrls).AsNoTracking()
                                         .Where(x => x.UserName == username)
                                         .FirstOrDefault();
        }
    }
}