namespace WheelsCrawler.Request
{
    public class WheelsCrawlerRequest : IWheelsCrawlerRequest
    {
        public string Url { get; set; }
        public string Regex { get; set; }
        public long TimeOut { get; set; }
    }
}