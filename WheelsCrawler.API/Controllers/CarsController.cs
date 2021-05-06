using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.unitOfWork;

namespace WheelsCrawler.API.Controllers
{
    public class CarsController : BaseApiController
    {
        private readonly IUnitOfWork _unityOfWork;
        private readonly IMapper _mapper;
        public CarsController(IUnitOfWork unityOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unityOfWork = unityOfWork;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<CarDto>> GetCars()
        {
            var cars = _unityOfWork.Cars.GetAll().AsEnumerable().ToList();
           
            var carsToReturn = _mapper.Map<List<CarDto>>(cars);

            return Ok(carsToReturn);
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