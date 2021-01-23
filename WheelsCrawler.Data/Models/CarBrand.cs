using System.Collections.Generic;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.Models
{
    public partial class CarBrand : IEntity
    {
        // public CarBrand()
        // {
        //     Cars = new HashSet<Car>();
        // }
        public int Id { get; set; }
        public string BrandName { get; set; }

        // public virtual ICollection<Car> Cars { get; set; }
    }
}