using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Models.Account;

namespace WheelsCrawler.Data.Repository
{
    public class AccManager : IAccManager
    {
        private readonly WheelsCrawlerDbContext _context;

        public AccManager(WheelsCrawlerDbContext context)
        {
            _context = context;
        }
        public void Create(User item)
        {
            _context.Users.Add(item);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}