using System.Collections.Generic;

namespace WheelsCrawler.Data.Models
{
    public class CarType
    {
        // public CarType()
        // {
        //     Cars = new HashSet<Car>();
        // }
        public int Id { get; set; }
        public string TypeName { get; set; }

        // public virtual ICollection<Car> Cars { get; set; }
    }
}