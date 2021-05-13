using System.Collections.Generic;
using WheelsCrawler.Data.Dto;

namespace WheelsCrawler.Data.Dto
{
    public class CarTypeDto
    {
        public int Id { get; set; }
        public string WheelsName { get; set; }
        public virtual ICollection<CarDto> Cars { get; set; }

    }
}