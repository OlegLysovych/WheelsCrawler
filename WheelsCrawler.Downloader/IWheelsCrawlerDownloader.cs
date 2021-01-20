using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WheelsCrawler.Downloader
{
    public interface IWheelsCrawlerDownloader
    {
        WheelsCrawlerDownloaderType DownloderType { get; set; }
        string DownloadPath { get; set; }
        Task<HtmlDocument> Download(string crawlUrl);
    }
}