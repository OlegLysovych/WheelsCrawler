using WheelsCrawler.Data.Attributes;
using WheelsCrawler.Data.Repository;
using System.Xml.XPath;

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

        // public virtual CarBrand CarBrand { get; set; }
        // public virtual CarType CarType { get; set; }
    }
}