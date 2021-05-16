using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using WheelsCrawler.API.Extensions;
using WheelsCrawler.API.Helpers;
using WheelsCrawler.API.Interfaces;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Helpers;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.unitOfWork;

namespace WheelsCrawler.API.SignalR
{
    public class SearchHub : Hub
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly ICarService _carService;
        private readonly ICrawlerService _crawlerService;
        public SearchHub(IUnitOfWork uof, IMapper mapper, ICarService carService, ICrawlerService crawlerService)
        {
            _crawlerService = crawlerService;
            _carService = carService;
            _mapper = mapper;
            _uof = uof;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var url = httpContext.Request.Query["url"].ToString();
            // var groupName = GetGroupName(Context.User.GetUserName(), url);

            // await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            // var client = Clients.Client(Context.ConnectionId);
            var userParams = new UserParams { ExactUrl = $"{url}" };
            var cars = await _carService.GetCars(userParams, Context.User.GetUserName());

            var pagedCars = await PagedList<Car>.CreateAsync(cars, userParams.PageNumber, userParams.PageSize);

            var carsToReturn = _mapper.Map<IEnumerable<CarDto>>(pagedCars);
            // httpContext.Response.AddPaginationHeader(pagedCars.CurrentPage, pagedCars.PageSize, pagedCars.TotalCount, pagedCars.TotalPages);
            // Response.AddPaginationHeader(pagedCars.CurrentPage, pagedCars.PageSize, pagedCars.TotalCount, pagedCars.TotalPages);

            await Clients.Caller.SendAsync("ReceiveCrawledCars", carsToReturn);
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await base.OnDisconnectedAsync(ex);
        }

        public async Task GetCars()
        {
            
        }
        public async Task CrawlCars(SearchRequestParams requestToSearch)
        {
            var currentUserName = Context.User.GetUserName();
            var user = _uof.Users.GetByUsername(currentUserName);
            var userToWorkWith = _mapper.Map<MemberDTO>(user);
            try
            {
                // var crawledUrl = await _crawlerService.Crawl(requestToSearch, userToWorkWith);
                await Clients.Caller.SendAsync("ReceiveCrawledCars");
                // if (requestToSearch.IsNeedToSave)
                // {
                //     user.InterestedUrls.Add(crawledUrl);//TODO: optional saving url!
                //     await _uof.Users.SaveAll();
                //     crawledUrl.InterestedUsers.Add(user);//TODO: optional saving url!
                //     await _uof.Urls.SaveAll();
                // }
                // await Clients.Caller.re("Crawled", )
                await Clients.Caller.SendAsync("ReceiveCrawledCars");
                // return RedirectToActionPermanent(actionName: "GetCars", controllerName: "Cars", new { ExactUrl = crawledUrl.UrlToScrape });
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        private string GetGroupName(string caller, string url)
        {
            var stringCompare = string.CompareOrdinal(caller, url) < 0;
            return stringCompare ? $"{caller}-{url}" : $"{url}-{caller}";
        }
    }
}