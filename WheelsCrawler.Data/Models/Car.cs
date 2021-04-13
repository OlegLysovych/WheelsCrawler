using WheelsCrawler.Data.Attributes;
using WheelsCrawler.Data.Repository;
using System.Xml.XPath;
using System.Diagnostics.CodeAnalysis;
using System;

namespace WheelsCrawler.Data.Models
{
    [WheelsCrawlerEntity(XPath = "/html/body/div[2]/div[6]")]
    public partial class Car : IEntity
    {
        public int Id { get; set; }

        public int CarBrandId { get; set; }

        public int CarTypeId { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string PictureUri { get; set; }

        public string CarUri { get; set; }

        public string Price { get; set; }

        public string Kilometrage { get; set; }

        public string EngineСapacity { get; set; }

        public string City { get; set; }

        public string Plate { get; set; }

        public string PublishDate { get; set; }

        public string GearBox { get; set; }

        // public virtual CarBrand CarBrand { get; set; }
        // public virtual CarType CarType { get; set; }

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