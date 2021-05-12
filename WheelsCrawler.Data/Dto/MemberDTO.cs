using System.Collections.Generic;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.Dto
{
    public class MemberDTO: IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public ICollection<UrlDto> InterestedUrls { get; set; }

    }
}