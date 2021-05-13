using System.Collections.Generic;
using System.Threading.Tasks;
using WheelsCrawler.API.Helpers;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Models.Account;

namespace WheelsCrawler.API.Interfaces
{
    public interface ICrawlerService
    {
        Task<Url> Crawl(SearchRequestParams requestToSearch, MemberDTO user);
    }
}