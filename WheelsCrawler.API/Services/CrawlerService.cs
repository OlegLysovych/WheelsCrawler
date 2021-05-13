using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WheelsCrawler.API.Helpers;
using WheelsCrawler.API.Interfaces;
using WheelsCrawler.Core;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Models.Account;
using WheelsCrawler.Data.unitOfWork;
using WheelsCrawler.Downloader;
using WheelsCrawler.Pipeline;
using WheelsCrawler.Processor;
using WheelsCrawler.Request;

namespace WheelsCrawler.API.Services
{
    public class CrawlerService : ICrawlerService
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CrawlerService(IUnitOfWork uof, IMapper mapper)
        {
            _mapper = mapper;
            _uof = uof;
        }

        private string RiaUrlBuilder(SearchRequestParams requestToSearch)
        {
            string url = String.Empty;

            var model = _uof.Models.GetModelByName(requestToSearch.Model);
            var brand = model.CarBrand;
            url = $"https://auto.ria.com/uk/legkovie/{brand.RiaName}/{model.RiaName}/";
            return url;
        }
        private string RstUrlBuilder(SearchRequestParams requestToSearch)
        {
            string url = String.Empty;

            var model = _uof.Models.GetModelByName(requestToSearch.Model);
            var brand = model.CarBrand;
            url = $"https://rst.ua/ukr/oldcars/{brand.RstName}/{model.RstName}/";
            return url;
        }

        public async Task<Url> Crawl(SearchRequestParams requestToSearch, MemberDTO user)
        {
            var userToWorkWith = _mapper.Map<User>(user);
            var url = _uof.Urls.GetByLinkName($"{requestToSearch.Brand.ToLower()}/{requestToSearch.Model.ToLower()}");
            if (url == null)
            {
                url = new Url
                {
                    UrlToScrape = $"{requestToSearch.Brand.ToLower()}/{requestToSearch.Model.ToLower()}",
                };
                // if (!user.InterestedUrls.Any(x => x.UrlToScrape.ToLower() == url.UrlToScrape.ToLower()))
                //     url.InterestedUsers = new List<User> { userToWorkWith };

                await _uof.Urls.CreateAsync(url);
                if (!await _uof.Urls.SaveAll())
                    throw new Exception("there are a problem with creating new url when crawl");
            }
            else
            {
                if (!user.InterestedUrls.Any(x => x.UrlToScrape.ToLower() == url.UrlToScrape.ToLower()))
                {
                    // url.InterestedUsers.Add(userToWorkWith);
                    // await _uof.Urls.Update(url.Id, url);
                    // if (!await _uof.Urls.SaveAll())
                    //     throw new Exception("there are a problem with updating new url when crawl");
                }
            }

            await RiaCrawl(requestToSearch, url);
            await RstCrawl(requestToSearch, url);

            return url;
        }

        private async Task RiaCrawl(SearchRequestParams requestToSearch, Url url)
        {
            var crawlerRia = new WheelsCrawler<CarSearchRiaDto, Car>()
                                 .AddRequest(new WheelsCrawlerRequest { Url = RiaUrlBuilder(requestToSearch), Regex = @"\?page=[0-9]+$", TimeOut = 5000 })
                                 .AddDownloader(new WheelsCrawlerDownloader { DownloderType = WheelsCrawlerDownloaderType.FromFile, DownloadPath = "C:/Users/PC/source/repos/WheelsCrawler/htmls/RIA/" })
                                 .AddProcessor(new WheelsCrawlerProcessor<CarSearchRiaDto, Car>(_uof.Cars) { })
                                 .AddPipeline(new WheelsCrawlerPipeline<Car>(_uof.Cars) { Url = url });

            await crawlerRia.Crawle();
        }
        private async Task RstCrawl(SearchRequestParams requestToSearch, Url url)
        {
            var crawlerRST = new WheelsCrawler<CarSearchRstDto, Car>()
                                 .AddRequest(new WheelsCrawlerRequest { Url = RstUrlBuilder(requestToSearch), Regex = @".*/oldcars/.+/[0-9]+\.html$", TimeOut = 5000 })
                                 .AddDownloader(new WheelsCrawlerDownloader { DownloderType = WheelsCrawlerDownloaderType.FromFile, DownloadPath = @"C:/Users/PC/source/repos/WheelsCrawler/htmls/RST/" })
                                 .AddProcessor(new WheelsCrawlerProcessor<CarSearchRstDto, Car>(_uof.Cars) { })
                                 .AddPipeline(new WheelsCrawlerPipeline<Car>(_uof.Cars) { Url = url });

            await crawlerRST.Crawle();
        }
    }
}