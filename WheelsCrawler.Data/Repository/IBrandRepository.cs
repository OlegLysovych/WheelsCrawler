using System.Linq;
using WheelsCrawler.Data.Models;

namespace WheelsCrawler.Data.Repository
{
    public interface IBrandRepository
    {
        IQueryable<CarBrand> GetAll();
        CarBrand GetBrandByName(string brandName);
    }
}