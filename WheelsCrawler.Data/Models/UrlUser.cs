using WheelsCrawler.Data.Models.Account;

namespace WheelsCrawler.Data.Models
{
    public class UrlUser
    {
        public int InterestedUrlsId { get; set; }
        public Url Url { get; set; }
        public int InterestedUsersId { get; set; }
        public User User { get; set; }
    }
}