using WheelsCrawler.Data.Attributes;
using WheelsCrawler.Data.Repository;
using System.Xml.XPath;

namespace WheelsCrawler.Data.Dto
{
    [WheelsCrawlerEntity(XPath = "/html/body/div[2]/div[6]")]
    public partial class CarRiaDto : IEntity
    {
        public int Id { get; set; }

        // [WheelsCrawlerField(Expression = "1", SelectorType = SelectorType.FixedValue)]
        public int CarBrandId { get; set; }

        // [WheelsCrawlerField(Expression = "1", SelectorType = SelectorType.FixedValue)]
        public int CarTypeId { get; set; }

        [WheelsCrawlerField(Expression = "//*[@id='full-description']/text()", SelectorType = SelectorType.XPath)]
        public string Description { get; set; }

        [WheelsCrawlerField(Expression = "/html/body/div[2]/div[6]/main/div[2]/h3", SelectorType = SelectorType.XPath)]
        public string Name { get; set; }

        [WheelsCrawlerField(Expression = "//*[@id='photosBlock']/div[1]/div[1]/div[1]/picture/img/@src", SelectorType = SelectorType.XPath)]
        public string PictureUri { get; set; }//*[@id='photosBlock']/div[1]/div[1]/div[1]/picture/img/@src

        [WheelsCrawlerField(Expression = "/html/head/link[74]/@href", SelectorType = SelectorType.XPath)]
        public string CarUri { get; set; }

        [WheelsCrawlerField(Expression = "/html/body/div[2]/div[6]/main/div[2]/section/span[1]", SelectorType = SelectorType.XPath)]
        public string Price { get; set; }

        [WheelsCrawlerField(Expression = "//*[@id='description_v3']/dl/dd[2]/span[2]", SelectorType = SelectorType.XPath)]
        public string Kilometrage { get; set; }

        [WheelsCrawlerField(Expression = "//*[@id='description_v3']/dl/dd[3]/span[2]", SelectorType = SelectorType.XPath)]
        public string EngineСapacity { get; set; }

        // public virtual CarBrand CarBrand { get; set; }
        // public virtual CarType CarType { get; set; }
    }
}