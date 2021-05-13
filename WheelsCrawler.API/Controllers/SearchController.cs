using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WheelsCrawler.API.DTO;
using WheelsCrawler.API.Extensions;
using WheelsCrawler.API.Interfaces;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Helpers;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Models.Account;
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
        [HttpGet]
        public async Task<ActionResult> Search([FromQuery] SearchRequest requestToSearch)
        {
            var currentUserName = User.GetUserName();
            var user = _uof.Users.GetByUsername(currentUserName);
            var userToWorkWith = _mapper.Map<MemberDTO>(user);
            try
            {
                var crawledUrl = await _crawlerService.Crawl(requestToSearch, userToWorkWith);
                user.InterestedUrls.Add(crawledUrl);//TODO: optional saving url!
                await _uof.Users.SaveAll();
                crawledUrl.InterestedUsers.Add(user);//TODO: optional saving url!
                await _uof.Urls.SaveAll();
                return RedirectToActionPermanent(actionName: "GetCars", controllerName: "Cars", new { ExactUrl = crawledUrl.UrlToScrape });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}