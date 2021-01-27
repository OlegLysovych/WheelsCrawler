using WheelsCrawler.Request;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;

namespace WheelsCrawler.Downloader
{
    /// <summary>
    /// Get Urls
    // https://codereview.stackexchange.com/questions/139783/web-crawler-that-uses-task-parallel-library 
    /// </summary>
    public class WheelsCrawlerPageLinkReader
    {
        private readonly IWheelsCrawlerRequest _request;
        private readonly Regex _regex;

        public WheelsCrawlerPageLinkReader(IWheelsCrawlerRequest request)
        {
            _request = request;
            if (!string.IsNullOrWhiteSpace(request.Regex))
            {
                _regex = new Regex(request.Regex);
            }
        }

        public async Task<IEnumerable<string>> GetLinks(string url, int level = 0)
        {
            if (level < 0)
                throw new ArgumentOutOfRangeException(nameof(level));

            var rootUrls = await GetPageLinks(url, false);

            if (level == 0)
                return rootUrls;

            var links = await GetAllPagesLinks(rootUrls);

            --level;
            var tasks = await Task.WhenAll(links.Select(link => GetLinks(link, level)));
            return tasks.SelectMany(l => l);
        }

        private async Task<IEnumerable<string>> GetPageLinks(string url, bool needMatch = true)
        {
            try
            {
                // HtmlWeb web = new HtmlWeb();
                // //Encoding encoding = Encoding.UTF8;
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                // if (url.Contains("rst"))
                // {
                //     //encoding = Encoding.UTF7;
                //     web = new HtmlWeb
                //     {
                //         AutoDetectEncoding = false,
                //         OverrideEncoding = Encoding.GetEncoding("windows-1251"),
                //     };
                //     WebClient wc = new WebClient();
                //     var str = wc.DownloadString(url);
                //     var htmlD = new HtmlAgilityPack.HtmlDocument();
                //     htmlD.LoadHtml(str);
                //     // web.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0";
                // }
                // web.UserAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http: //www.google.com/bot.html)";// 
                // var htmlDocument = await web.LoadFromWebAsync(url);
                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                switch (url)
                {
                    case string c when c.Contains("rst"):
                        WebClient wc = new WebClient();
                        wc.Encoding = System.Text.Encoding.GetEncoding(1251);
                        wc.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; Googlebot/2.1; +http: //www.google.com/bot.html)");
                        var str = await wc.DownloadStringTaskAsync(c);

                        htmlDocument.LoadHtml(str);
                        break;
                    case string a when !a.Contains("rst"):
                        // case string a when a.Contains("mobile"):
                        HtmlWeb web = new HtmlWeb();
                        web.UserAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http: //www.google.com/bot.html)";// 
                        htmlDocument = await web.LoadFromWebAsync(a);
                        break;
                    default:
                        throw new Exception("Create a more multipurpose method, lazy piece of ..");
                }

                var linkList = htmlDocument.DocumentNode
                                   .Descendants("a")
                                   .Select(a => a.GetAttributeValue("href", null))
                                   .Where(u => !string.IsNullOrEmpty(u))
                                   .Distinct();

                if (_regex != null)
                    linkList = linkList.Where(x => _regex.IsMatch(x));

                if (url.Contains("rst"))
                    // linkList.ToList().ForEach(x => x = "https://rst.ua" + x);//x = x.Insert(0, "rst.ua")\ 
                    // linkList.ToList().ForEach(x => x = x.Insert(0,"https://rst.ua"));//x = x.Insert(0, "rst.ua")\ 
                    foreach (var item in linkList)
                    {
                        linkList = linkList.Select(x => x.Equals(item) ? $"https://rst.ua{x}" : x);

                        // linkList.Where(x => x.Equals(item)).
                        // item.Insert(0, "https://rst.ua");
                    }

                return linkList;
            }
            catch (Exception exception)
            {
                return Enumerable.Empty<string>();
            }
        }

        private async Task<IEnumerable<string>> GetAllPagesLinks(IEnumerable<string> rootUrls)
        {
            var result = await Task.WhenAll(rootUrls.Select(url => GetPageLinks(url)));

            return result.SelectMany(x => x).Distinct();
        }
    }
}
