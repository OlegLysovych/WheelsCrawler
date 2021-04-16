using WheelsCrawler.Data.Attributes;
using WheelsCrawler.Data.Repository;
using System.Xml.XPath;
using System.Diagnostics.CodeAnalysis;

namespace WheelsCrawler.Data.Dto
{
    [WheelsCrawlerEntity(XPath = "//*[contains(@class,'ticket-item')]")]// /html/body/div[5]/section[1]/div[2]/div/div/section[16]
    public partial class CarSearchRiaDto : IEntity
    {
        public int Id { get; set; }

        // [WheelsCrawlerField(Expression = "1", SelectorType = SelectorType.FixedValue)]
        public int CarBrandId { get; set; }

        // [WheelsCrawlerField(Expression = "1", SelectorType = SelectorType.FixedValue)]
        public int CarTypeId { get; set; }

        [WheelsCrawlerField(Expression = ".//*[contains(@class,'ticket-title')]/a/span", SelectorType = SelectorType.XPath)]//item ticket-title
        public string Name { get; set; }

        [WheelsCrawlerField(Expression = ".//*[contains(@class,'descriptions')]/span/text()", SelectorType = SelectorType.XPath)]//descriptions-ticket show-desc
        public string Description { get; set; }

        [WheelsCrawlerField(Expression = ".//*[contains(@class,'ticket-photo')]/a/picture/img/@src", SelectorType = SelectorType.XPath)]//*[@id='photosBlock']/div[1]/div[1]//picture/img/@src
        public string PictureUri { get; set; }

        [WheelsCrawlerField(Expression = ".//*[contains(@class,'ticket-title')]/a/@href", SelectorType = SelectorType.XPath)]//*[@id="searchResults"]/section[1]/div[4]/div[2]/div[1]/div/a
        public string CarUri { get; set; }

        [WheelsCrawlerField(Expression = ".//*[contains(@class,'price-ticket')]/span[1]/span[1]", SelectorType = SelectorType.XPath)]//*[contains(@class,'price-ticket')]/span[1]/span[1]/text()
        public string Price { get; set; }

        [WheelsCrawlerField(Expression = ".//*[contains(@class,'definition-data')]/ul[1]/li[1]", SelectorType = SelectorType.XPath)]
        public string Kilometrage { get; set; }

        [WheelsCrawlerField(Expression = ".//*[contains(@class,'definition-data')]/ul[1]/li[3]", SelectorType = SelectorType.XPath)]
        public string EngineСapacity { get; set; }

        [WheelsCrawlerField(Expression = ".//*[contains(@class,'definition-data')]/ul[1]/li[2]/text()[2]", SelectorType = SelectorType.XPath)]
        public string City { get; set; }

        [WheelsCrawlerField(Expression = ".//*[contains(@class,'base_information')]/span/text()", SelectorType = SelectorType.XPath)]
        public string Plate { get; set; }

        [WheelsCrawlerField(Expression = ".//*[contains(@class,'footer_ticket')]/span/@data-add-date", SelectorType = SelectorType.XPath)]
        public string PublishDate { get; set; }//*[@id="searchResults"]/section[1]/div[4]/div[2]/div[4]/span

        [WheelsCrawlerField(Expression = ".//*[contains(@class,'definition-data')]/ul[1]/li[4]", SelectorType = SelectorType.XPath)]
        public string GearBox { get; set; }

    }
}