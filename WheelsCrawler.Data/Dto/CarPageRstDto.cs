using WheelsCrawler.Data.Attributes;
using WheelsCrawler.Data.Repository;
using System.Xml.XPath;

namespace WheelsCrawler.Data.Dto
{
    [WheelsCrawlerEntity(XPath = "//*[@id='rst-page-oldcars-item']")]
    public partial class CarPageRstDto : IEntity
    {
        public int Id { get; set; }

        // [WheelsCrawlerField(Expression = "1", SelectorType = SelectorType.FixedValue)]
        public int CarBrandId { get; set; }

        // [WheelsCrawlerField(Expression = "1", SelectorType = SelectorType.FixedValue)]
        public int CarTypeId { get; set; }

        [WheelsCrawlerField(Expression = "//*[@id='rst-page-oldcars-item-option-block-container-desc']/text()", SelectorType = SelectorType.XPath)]
        public string Description { get; set; }//*[@id="rst-page-oldcars-item-option-block-container-desc"]

        [WheelsCrawlerField(Expression = "//*[@id='rst-page-oldcars-item-header']/text()", SelectorType = SelectorType.XPath)]
        public string Name { get; set; }

        [WheelsCrawlerField(Expression = "//*[@id='rst-page-oldcars-mainphoto']/@src", SelectorType = SelectorType.XPath)]
        public string PictureUri { get; set; }

        [WheelsCrawlerField(Expression = "/html/head/link[4]/@href", SelectorType = SelectorType.XPath)]
        public string CarUri { get; set; }

        [WheelsCrawlerField(Expression = "//span[@class='rst-uix-price-param']/span", SelectorType = SelectorType.XPath)]
        public string Price { get; set; }//*[@id="rst-page-oldcars-item"]/div[2]/ul/li[1]/span[2]/span/span

        [WheelsCrawlerField(Expression = "//*[@id='rst-page-oldcars-item']/div[2]/ul/li[2]/span[2]/span", SelectorType = SelectorType.XPath)]
        public string Kilometrage { get; set; }//*[@id="rst-page-oldcars-item"]/div[2]/ul/li[2]/span[2]/span

        [WheelsCrawlerField(Expression = "//*[@id='rst-page-oldcars-item']/div[2]/ul/li[3]/span[2]/strong", SelectorType = SelectorType.XPath)]
        public string EngineСapacity { get; set; }

        // public virtual CarBrand CarBrand { get; set; }
        // public virtual CarType CarType { get; set; }
    }
}