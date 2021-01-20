using WheelsCrawler.Data.Attributes;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.Models
{
    public partial class Car : IEntity
    {
        [WheelsCrawlerEntity(XPath = "//*[@id='LeftSummaryPanel']/div[1]")]
        public int Id { get; set; }
        [WheelsCrawlerField(Expression = "1", SelectorType = SelectorType.FixedValue)]
        public int CatalogBrandId { get; set; }
        [WheelsCrawlerField(Expression = "1", SelectorType = SelectorType.FixedValue)]
        public int CatalogTypeId { get; set; }
        public string Description { get; set; }
        [WheelsCrawlerField(Expression = "//*[@id='itemTitle']/text()", SelectorType = SelectorType.XPath)]
        public string Name { get; set; }
        public string PictureUri { get; set; }
        public decimal Price { get; set; }

        public virtual CarBrand CarBrand { get; set; }
        public virtual CarType CarType { get; set; }
    }
}