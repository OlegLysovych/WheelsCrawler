using System.Linq;
using System.Threading.Tasks;
using WheelsCrawler.Data.Models;

namespace WheelsCrawler.Data.Repository
{
    public interface IUrlRepository : IGenericRepository<Url>
    {
        Url GetByLinkName(string link);
        new IQueryable<Url> GetAll();
    }
}