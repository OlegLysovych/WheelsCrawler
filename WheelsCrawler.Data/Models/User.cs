using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.Models
{
    public class User: IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}