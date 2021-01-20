namespace WheelsCrawler.Request
{
    public interface IWheelsCrawlerRequest
    {
         string Url { get; set; }
         string Regex { get; set; }
         long TimeOut { get; set; }
    }
}