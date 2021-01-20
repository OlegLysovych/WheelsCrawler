using System;
using System.Collections.Generic;
using System.Text;
using WheelsCrawler.Data.Attributes;

namespace WheelsCrawler.Data.Attributes
{
    public class WheelsCrawlerFieldAttribute : Attribute
    {
        public string Expression { get; set; }
        public SelectorType SelectorType { get; set; }
    }
}
