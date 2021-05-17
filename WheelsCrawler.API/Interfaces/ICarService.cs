using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Helpers;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Models.Account;

namespace WheelsCrawler.API.Interfaces
{
    public interface ICarService
    {
        Task<IQueryable<Car>> GetCars(UserParams userParams, string currentUserName);
    }
}