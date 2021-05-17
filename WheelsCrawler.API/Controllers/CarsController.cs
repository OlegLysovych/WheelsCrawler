using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WheelsCrawler.API.Extensions;
using WheelsCrawler.API.Interfaces;
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
        private readonly ICarService _carService;
        public CarsController(IUnitOfWork unityOfWork, IMapper mapper, ICarService carService)
        {
            _carService = carService;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCars([FromQuery] UserParams userParams)
        {
            // var cars = await _unityOfWork.Cars.GetAll(userParams);

            //TODO: move all this stuff to service!!!!!!!!!!!!

            var currentUserName = User.GetUserName();

            var cars = await _carService.GetCars(userParams, currentUserName);

            var pagedCars = await PagedList<Car>.CreateAsync(cars, userParams.PageNumber, userParams.PageSize);

            var carsToReturn = _mapper.Map<IEnumerable<CarDto>>(pagedCars);

            Response.AddPaginationHeader(pagedCars.CurrentPage, pagedCars.PageSize, pagedCars.TotalCount, pagedCars.TotalPages);

            return Ok(carsToReturn);
        }

        [Authorize]
        [HttpGet("cars/{url}")]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCarsByExactUrl(string url)
        {
            var cars = _unityOfWork.Repository<Car>().GetAll();

            //TODO: move all this stuff to service!!!!!!!!!!!!

            var currentUserName = User.GetUserName();
            var user = _unityOfWork.Users.GetByUsername(currentUserName);

            var userParams = new UserParams();

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