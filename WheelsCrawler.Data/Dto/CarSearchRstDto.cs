using WheelsCrawler.Data.Attributes;
using WheelsCrawler.Data.Repository;
using System.Xml.XPath;
using System.Diagnostics.CodeAnalysis;
using WheelsCrawler.Data.Models;
using System;

namespace WheelsCrawler.Data.Dto
{
    [WheelsCrawlerEntity(XPath = "//*[contains(@id,'rst-ocid')]")]//*[@id="rst-ocid-12289048"]
    public partial class CarSearchRstDto : IEntity
    {
        public int Id { get; set; }

        // [WheelsCrawlerField(Expression = "1", SelectorType = SelectorType.FixedValue)]
        public int CarBrandId { get; set; }

        // [WheelsCrawlerField(Expression = "1", SelectorType = SelectorType.FixedValue)]
        public int CarTypeId { get; set; }

        [WheelsCrawlerField(Expression = ".//*[@class='rst-ocb-i-d-d']/text()", SelectorType = SelectorType.XPath)]//
        public string Description { get; set; }//*[@id="rst-page-oldcars-item-option-block-container-desc"]

        [WheelsCrawlerField(Expression = ".//*[@class='rst-ocb-i-h']/span/text()", SelectorType = SelectorType.XPath)]
        public string Name { get; set; }

        [WheelsCrawlerField(Expression = ".//*[@class='rst-ocb-i-i']/img/@src", SelectorType = SelectorType.XPath)]
        public string PictureUri { get; set; }

        [WheelsCrawlerField(Expression = ".//*[@class='rst-ocb-i-a']/@href", SelectorType = SelectorType.XPath)]
        public string CarUri { get; set; }

        [WheelsCrawlerField(Expression = ".//span[@class='rst-uix-grey']", SelectorType = SelectorType.XPath)]
        public string Price { get; set; }//*[@id="rst-page-oldcars-item"]/div[2]/ul/li[1]/span[2]/span/span

        [WheelsCrawlerField(Expression = ".//*[@class='rst-ocb-i-d-l']/li[3]/text()[2]", SelectorType = SelectorType.XPath)]
        public string Kilometrage { get; set; }//*[@id="rst-page-oldcars-item"]/div[2]/ul/li[2]/span[2]/span

        [WheelsCrawlerField(Expression = ".//*[@class='rst-ocb-i-d-l']/li[5]/span[1]", SelectorType = SelectorType.XPath)]
        public string EngineСapacity { get; set; }

        [WheelsCrawlerField(Expression = ".//*[@class='rst-ocb-i-d-l']/li[2]/span", SelectorType = SelectorType.XPath)]
        public string City { get; set; }

        // [WheelsCrawlerField(Expression = ".//*[contains(@class,'base_information')]/span/text()", SelectorType = SelectorType.XPath)]
        // public string Plate { get; set; }

        [WheelsCrawlerField(Expression = "//*[@class='rst-ocb-i-s']", SelectorType = SelectorType.XPath)]
        public string PublishDate { get; set; }//*[@id="searchResults"]/section[1]/div[4]/div[2]/div[4]/span

        [WheelsCrawlerField(Expression = "//*[@class='rst-ocb-i-d-l']/li[5]/span[2]", SelectorType = SelectorType.XPath)]
        public string GearBox { get; set; }

    }
}