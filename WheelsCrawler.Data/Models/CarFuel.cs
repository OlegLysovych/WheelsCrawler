using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.Models
{
    public class CarFuel : IEntity
    {
        public int Id { get; set; }
        public string WheelsName { get; set; }
        public string RiaName { get; set; }
        public string RstName { get; set; }
    }
}