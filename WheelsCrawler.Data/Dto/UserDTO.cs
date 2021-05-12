using System.Collections.Generic;

namespace WheelsCrawler.Data.Dto
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public ICollection<UrlDto> InterestedUrls { get; set; }
    }
}