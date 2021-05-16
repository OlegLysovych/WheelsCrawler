using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WheelsCrawler.API.Interfaces;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Helpers;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Models.Account;
using WheelsCrawler.Data.unitOfWork;

namespace WheelsCrawler.API.Services
{
    public class CarService : ICarService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;
        public CarService(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public CarService()
        {
        }

        public async Task<IQueryable<Car>> GetCars(UserParams userParams, string currentUserName = "notNeeded")
        {
            var cars = await _uof.Cars.GetAll(userParams);

            //TODO: move all this stuff to service!!!!!!!!!!!!

            if (!String.IsNullOrEmpty(userParams.ExactUrl) && !currentUserName.Equals("notNeeded"))
            {
                // var urls = _uof.Repository<Url>().GetAll();
                // var url = urls.SingleOrDefault(x => x.UrlToScrape == userParams.ExactUrl);
                cars = cars.Where(x => x.RelatedQueryUrl.UrlToScrape.ToLower() == userParams.ExactUrl.ToLower());
            }
            else
            {
                var user = _uof.Users.GetByUsername(currentUserName);
                cars = cars.Where(x => user.InterestedUrls.Contains(x.RelatedQueryUrl));
            }


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


            return cars;
        }
    }
}