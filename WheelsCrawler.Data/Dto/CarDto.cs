using System;

namespace WheelsCrawler.Data.Dto
{
    public class CarDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string PictureUri { get; set; }

        public string CarUri { get; set; }

        public double Price { get; set; }

        public int Kilometrage { get; set; }

        public double Enginecapacity { get; set; }

        public string City { get; set; }

        public string Plate { get; set; }

        public string Publishdate { get; set; }
        public string Cargearbox { get; set; }
        public string Carbrand { get; set; }
        public string Cartype { get; set; }
        public string Carfuel { get; set; }
        public string Carmodel { get; set; }
    }
}