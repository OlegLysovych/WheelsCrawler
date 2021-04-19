using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.unitOfWork;

namespace WheelsCrawler.API.Controllers
{
    public class CarsController : BaseApiController
    {
        private readonly IUnitOfWork _unityOfWork;
        public CarsController(IUnitOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        [Authorize]
        [HttpGet]
        public IEnumerable<Car> GetCars()
        {
            var cars = _unityOfWork.Repository<Car>().GetAll().AsEnumerable();
            return cars;
        }
        // [HttpPost]
        // public async Task<IActionResult> PostBrand()
        // {
        //     CarBrand carBrand = new CarBrand { WheelsName = "Kia", RiaName = "Kia", RstName = "Kia" };
        //     var cars = _unityOfWork.Repository<CarBrand>().CreateAsync(carBrand);
        //     if (await _unityOfWork.Repository<CarBrand>().SaveAll())
        //         return Ok();
        //     return BadRequest("failed to add new brand");
        // }
    }
}