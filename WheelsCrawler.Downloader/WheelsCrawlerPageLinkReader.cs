using WheelsCrawler.Request;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.IO;

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
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                CookieCollection cookieCollection = null;
                switch (url)
                {
                    case string c when c.Contains("rst")://do it wothout hardcode, maybe test call, check encoding, set here and call again to scrap?
                        WebClient wc = new WebClient();
                        wc.Encoding = System.Text.Encoding.GetEncoding(1251);
                        wc.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)");
                        string str = String.Empty;
                        try
                        {
                            str = await wc.DownloadStringTaskAsync(c);
                        }
                        catch (System.Exception)
                        {
                            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                            req.Method = "GET";
                            req.AllowAutoRedirect = true;
                            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
                            HttpWebResponse webresponse = (HttpWebResponse)req.GetResponse();
                            Console.Write(webresponse.StatusCode); //check the statusCode
                            Stream receiveStream = webresponse.GetResponseStream();
                            using (StreamReader reader = new StreamReader(receiveStream))
                            {
                                str = reader.ReadToEnd();
                            }
                        }

                        htmlDocument.LoadHtml(str);
                        break;
                    // CookieCollection cookieCollection = null;
                    // HtmlWeb webC = new HtmlWeb
                    // {
                    //     UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.146 Safari/537.36",
                    //     UseCookies = true,
                    //     OverrideEncoding = System.Text.Encoding.GetEncoding(1251),
                    //     PreRequest = request =>
                    //     {
                    //         if (cookieCollection != null && cookieCollection.Count > 0)
                    //             request.CookieContainer.Add(cookieCollection);
                    //         return true;
                    //     },
                    //     PostResponse = (request, response) =>
                    //     {
                    //         cookieCollection = response.Cookies;
                    //     }
                    // };

                    // htmlDocument = await webC.LoadFromWebAsync(c);
                    // break;
                    case string a when !a.Contains("rst"):
                        // case string a when a.Contains("mobile"):
                        HtmlWeb web = new HtmlWeb
                        {
                            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.146 Safari/537.36",
                            UseCookies = true,
                            PreRequest = request =>
                            {
                                if (cookieCollection != null && cookieCollection.Count > 0)
                                    request.CookieContainer.Add(cookieCollection);
                                return true;
                            },
                            PostResponse = (request, response) => { cookieCollection = response.Cookies; }
                        };

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
                {
                    foreach (var item in linkList)
                    {
                        linkList = linkList.Select(x => x.Equals(item) ? $"https://rst.ua{x}" : x);
                    }
                }

                if (!url.Contains("rst") && linkList.Count() > 4)
                {
                    var lastPage = Int32.Parse(Regex.Match(linkList.Last(), @"\d+$").Value);
                    var secondLastPage = linkList.Count() > 1 ? Int32.Parse(Regex.Match(linkList.SkipLast(1).Last(), @"\d+$").Value) : 0;
                    if (lastPage - secondLastPage >= 1)
                    {
                        var newUrl = linkList.SkipLast(1).Last();
                        for (int i = secondLastPage + 1; i < lastPage; i++)
                        {
                            var urlToAdd = newUrl;
                            urlToAdd = Regex.Replace(urlToAdd, @"\d+$", i.ToString());
                            linkList = linkList.Append(urlToAdd);
                        }
                    }
                }
                linkList = linkList.Prepend(url);

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
