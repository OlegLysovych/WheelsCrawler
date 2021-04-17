using System.Collections.Generic;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.Models
{
    public class CarType : IEntity
    {
        public CarType() => Cars = new HashSet<Car>();
        public int Id { get; set; }
        public string WheelsName { get; set; }
        public string RiaName { get; set; }
        public string RstName { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}