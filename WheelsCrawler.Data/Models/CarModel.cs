using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.Models
{
    public class CarModel : IEntity
    {
        public int Id { get; set; }
        public string WheelsName { get; set; }
        public string RiaName { get; set; }
        public string RstName { get; set; }
        public int CarBrandId { get; set; }

        public virtual CarBrand CarBrand { get; set; }
    }
}