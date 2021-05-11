using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WheelsCrawler.API.Extensions;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Helpers;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Models.Account;
using WheelsCrawler.Data.unitOfWork;
// eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJzdGVwaGFuIiwibmJmIjoxNjIwMzc3OTk3LCJleHAiOjE2MjA5ODI3OTcsImlhdCI6MTYyMDM3Nzk5N30.MWRvxVpxZ3fkGWNbWMeFuUmi8JoyWzeOaTiPIiMgl35O4jFms2AViw8Q0L4ZS82WnLd1Zt6HehzgWfEKgECTYA
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
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCars([FromQuery] UserParams userParams)
        {
            var cars = await _unityOfWork.Cars.GetAll(userParams);

            //TODO: move all this stuff to service!!!!!!!!!!!!

            var currentUserName = User.GetUserName();
            var user = _unityOfWork.Users.GetByUsername(currentUserName);

            cars = cars.Where(x => user.InterestedUrls.Contains(x.RelatedQueryUrl));

            if (userParams.PriceFrom != 0)
                cars = cars.Where(x => x.Price >= userParams.PriceFrom);

            if (userParams.PriceTo != 1_000_000)
                cars = cars.Where(x => x.Price <= userParams.PriceTo);

            if (userParams.EngineCapacityFrom != 0.0)
                cars = cars.Where(x => x.EngineСapacity >= userParams.EngineCapacityFrom);

            if (userParams.EngineCapacityTo != 10.0)
                cars = cars.Where(x => x.EngineСapacity <= userParams.EngineCapacityTo);

            if (userParams.KilometrageFrom != 0)
                cars = cars.Where(x => x.Kilometrage >= userParams.KilometrageFrom);

            if (userParams.KilometrageFrom != 1_000_000)
                cars = cars.Where(x => x.Kilometrage <= userParams.KilometrageTo);

            if (!String.IsNullOrEmpty(userParams.City))
                cars = cars.Where(x => x.City == userParams.City);

            cars = userParams.OrderBy switch 
            {
                "highestKilometrage" => cars.OrderByDescending(x => x.Kilometrage),
                "lowestKilometrage" => cars.OrderBy(x => x.Kilometrage),
                "highestPrice" => cars.OrderByDescending(x => x.Price),
                "lowestPrice" => cars.OrderBy(x => x.Price),
                _ => cars.OrderByDescending(x => x.PublishDate),
            };

            var pagedCars = await PagedList<Car>.CreateAsync(cars, userParams.PageNumber, userParams.PageSize);

            var carsToReturn = _mapper.Map<IEnumerable<CarDto>>(pagedCars);

            Response.AddPaginationHeader(pagedCars.CurrentPage, pagedCars.PageSize, pagedCars.TotalCount, pagedCars.TotalPages);

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