using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WheelsCrawler.API.Extensions;
using WheelsCrawler.API.Helpers;
using WheelsCrawler.API.Interfaces;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.unitOfWork;

namespace WheelsCrawler.API.Controllers
{
    public class SearchController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;
        private readonly ICrawlerService _crawlerService;
        public SearchController(IUnitOfWork uof, IMapper mapper, ICrawlerService crawlerService)
        {
            _crawlerService = crawlerService;
            _uof = uof;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("Crawl")]
        public async Task<ActionResult> Search([FromQuery] SearchRequestParams requestToSearch)
        {
            var currentUserName = User.GetUserName();
            var user = _uof.Users.GetByUsername(currentUserName);
            var userToWorkWith = _mapper.Map<MemberDTO>(user);
            try
            {
                var crawledUrl = await _crawlerService.Crawl(requestToSearch, userToWorkWith);
                if (requestToSearch.IsNeedToSave)
                {
                    user.InterestedUrls.Add(crawledUrl);//TODO: optional saving url!
                    await _uof.Users.SaveAll();
                    crawledUrl.InterestedUsers.Add(user);//TODO: optional saving url!
                    await _uof.Urls.SaveAll();
                }
                return RedirectToActionPermanent(actionName: "GetCars", controllerName: "Cars", new { ExactUrl = crawledUrl.UrlToScrape });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("brands")]
        public ActionResult<IEnumerable<CarBrandDto>> GetBrands()
        {
            var brands = _uof.Brands.GetAll().ToList();
            var brandsToReturn = _mapper.Map<IEnumerable<CarBrandDto>>(brands);
            return Ok(brandsToReturn);
        }
        [Authorize]
        [HttpGet("models")]
        public ActionResult<IEnumerable<CarModelDto>> GetModels()
        {
            var models = _uof.Repository<CarModel>().GetAll().ToList();
            var modelsToReturn = _mapper.Map<IEnumerable<CarModelDto>>(models);
            return Ok(modelsToReturn);
        }
        [Authorize]
        [HttpGet("fuels")]
        public ActionResult<IEnumerable<CarFuelDto>> GetFuels()
        {
            var fuels = _uof.Repository<CarFuel>().GetAll().ToList();
            var fuelsToReturn = _mapper.Map<IEnumerable<CarFuelDto>>(fuels);
            return Ok(fuelsToReturn);
        }
        [Authorize]
        [HttpGet("gearboxes")]
        public ActionResult<IEnumerable<CarGearboxDto>> GetGearboxes()
        {
            var gearboxes = _uof.Repository<CarGearbox>().GetAll().ToList();
            var gearboxesToReturn = _mapper.Map<IEnumerable<CarGearboxDto>>(gearboxes);
            return Ok(gearboxesToReturn);
        }
        [Authorize]
        [HttpGet("types")]
        public ActionResult<IEnumerable<CarTypeDto>> GetTypes()
        {
            var types = _uof.Repository<CarType>().GetAll().ToList();
            var typesToReturn = _mapper.Map<IEnumerable<CarTypeDto>>(types);
            return Ok(typesToReturn);
        }
    }
}