using WheelsCrawler.Data.Attributes;
using WheelsCrawler.Data.Repository;
using System.Xml.XPath;
using System.Diagnostics.CodeAnalysis;
using System;

namespace WheelsCrawler.Data.Models
{
    public partial class Car : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string PictureUri { get; set; }

        public string CarUri { get; set; }

        public decimal Price { get; set; }

        public int Kilometrage { get; set; }

        public double EngineСapacity { get; set; } 

        public string City { get; set; }

        public string Plate { get; set; }

        public DateTime PublishDate { get; set; }

        
        public virtual CarGearbox CarGearbox { get; set; }
        public virtual CarBrand CarBrand { get; set; }
        public virtual CarType CarType { get; set; }
        public virtual CarFuel CarFuel { get; set; }
        public virtual CarModel CarModel { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Car b = (Car)obj;
            return CarUri.Equals(b.CarUri);
        }

        public override int GetHashCode()
        {
            return CarUri.GetHashCode() ^ PictureUri.GetHashCode();
        }
    }
}