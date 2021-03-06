using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WheelsCrawler.Downloader
{
    public class WheelsCrawlerDownloader : IWheelsCrawlerDownloader
    {
        public WheelsCrawlerDownloaderType DownloderType { get; set; }
        public string DownloadPath { get; set; }
        private string _localFilePath;

        public WheelsCrawlerDownloader()
        {

        }

        public async Task<HtmlDocument> Download(string crawlUrl)
        {
            // if exist dont download again
            PrepareFilePath(crawlUrl);

            var existing = GetExistingFile(_localFilePath);
            if (existing != null)
                return existing;

            return await DownloadInternal(crawlUrl);
        }

        private async Task<HtmlDocument> DownloadInternal(string crawlUrl)
        {
            switch (DownloderType)
            {
                case WheelsCrawlerDownloaderType.FromFile:
                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; Googlebot/2.1; +http: //www.google.com/bot.html)");
                        if (crawlUrl.Contains("rst"))
                            client.Encoding = System.Text.Encoding.GetEncoding(1251);
                        await client.DownloadFileTaskAsync(crawlUrl, _localFilePath);
                    }
                    return GetExistingFile(_localFilePath);

                case WheelsCrawlerDownloaderType.FromMemory:
                    var htmlDocument = new HtmlDocument();
                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; Googlebot/2.1; +http: //www.google.com/bot.html)");
                        if (crawlUrl.Contains("rst"))
                            client.Encoding = System.Text.Encoding.GetEncoding(1251);
                        string htmlCode = await client.DownloadStringTaskAsync(crawlUrl);
                        htmlDocument.LoadHtml(htmlCode);
                    }
                    return htmlDocument;

                case WheelsCrawlerDownloaderType.FromWeb:

                    htmlDocument = new HtmlDocument();
                    WebClient wc = new WebClient();
                    if (crawlUrl.Contains("rst"))
                        wc.Encoding = System.Text.Encoding.GetEncoding(1251);
                    wc.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; Googlebot/2.1; +http: //www.google.com/bot.html)");
                    var str = await wc.DownloadStringTaskAsync(crawlUrl);

                    htmlDocument.LoadHtml(str);
                    return htmlDocument;
                    // HtmlWeb web = new HtmlWeb();
                    // web.UserAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http: //www.google.com/bot.html)";
                    // if (crawlUrl.Contains("rst"))
                    //     web.OverrideEncoding = System.Text.Encoding.GetEncoding(1251);
                    // return await web.LoadFromWebAsync(crawlUrl);
            }

            throw new InvalidOperationException("Can not load html file from given source.");
        }

        private void PrepareFilePath(string fileName)
        {
            var parts = fileName.Split('/');
            parts = parts.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
            var htmlpage = string.Empty;
            if (parts.Length > 0)
            {
                htmlpage = parts[parts.Length - 3] + "_" + parts[parts.Length - 2] + "_" + parts[parts.Length - 1];
            }

            if (!htmlpage.Contains(".html"))
            {
                htmlpage = htmlpage + ".html";
            }
            htmlpage = htmlpage.Replace("=", "").Replace("?", "");

            _localFilePath = $"{DownloadPath}{htmlpage}";
        }

        private HtmlDocument GetExistingFile(string fullPath)
        {
            try
            {
                var htmlDocument = new HtmlDocument();
                if (fullPath.Contains("RST"))
                    htmlDocument.Load(fullPath, System.Text.Encoding.GetEncoding(1251));
                else
                    htmlDocument.Load(fullPath);

                return htmlDocument;
            }
            catch (Exception exception)
            {
            }
            return null;
        }

    }
}