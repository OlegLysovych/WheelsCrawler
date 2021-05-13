using System.Collections.Generic;

namespace WheelsCrawler.Data.Dto
{
    public class CarBrandDto
    {
        public int Id { get; set; }
        public string WheelsName { get; set; }
        public virtual ICollection<CarModelDto> CarModels { get; set; }

    }
}