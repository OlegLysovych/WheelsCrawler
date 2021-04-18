using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.unitOfWork;

namespace WheelsCrawler.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly IUnitOfWork _unityOfWork;
        public CarsController(IUnitOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetCars()
        {
            var cars = _unityOfWork.Repository<Car>().GetAll().AsEnumerable();
            return Ok(cars);
        }
    }
}