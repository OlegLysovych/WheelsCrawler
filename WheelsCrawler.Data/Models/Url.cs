using System.ComponentModel.DataAnnotations.Schema;

namespace WheelsCrawler.Data.Models
{
    [Table("Urls")]
    public class Url
    {
        public int Id { get; set; }
        public string UrlToScrape { get; set; }
    }
}