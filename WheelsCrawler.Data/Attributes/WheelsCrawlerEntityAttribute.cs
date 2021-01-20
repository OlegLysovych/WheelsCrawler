using System;
using System.Collections.Generic;
using System.Text;

namespace WheelsCrawler.Data.Attributes
{
    public class WheelsCrawlerEntityAttribute : Attribute
    {
        public string XPath { get; set; }
    }    
}
