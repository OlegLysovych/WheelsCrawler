using System.Collections.Generic;

namespace WheelsCrawler.Data.Models
{
    public class CarBrand
    {
        public CarBrand() => CarModels = new HashSet<CarModel>();
        public int Id { get; set; }
        public string WheelsName { get; set; }
        public string RiaName { get; set; }
        public string RstName { get; set; }

        public virtual ICollection<CarModel> CarModels { get; set; }
    }
}