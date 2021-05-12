using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WheelsCrawler.Data.Models.Account;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.Models
{
    [Table("Urls")]
    public class Url: IEntity
    {
        public int Id { get; set; }
        public string UrlToScrape { get; set; }
        public virtual ICollection<User> InterestedUsers { get; set; }
    }
}