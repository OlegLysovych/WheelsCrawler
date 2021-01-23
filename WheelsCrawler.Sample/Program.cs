using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WheelsCrawler.Core;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Downloader;
using WheelsCrawler.Pipeline;
using WheelsCrawler.Processor;
using WheelsCrawler.Request;

namespace WheelsCrawler.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }
        static async Task MainAsync(string[] args)
        {
            var crawler = new WheelsCrawler<Car>()
                                 .AddRequest(new WheelsCrawlerRequest { Url = "https://auto.ria.com/uk/search/?indexName=auto,order_auto,newauto_search&categories.main.id=1&brand.id[0]=48&model.id[0]=1710&price.currency=1&sort[0].order=dates.created.desc&abroad.not=0&custom.not=1&page=0&size=10", Regex = @".*/auto_.+", TimeOut = 5000 })
                                 .AddDownloader(new WheelsCrawlerDownloader { DownloderType = WheelsCrawlerDownloaderType.FromFile, DownloadPath = "C:/Users/PC/source/repos/WheelsCrawler/html`s/"})
                                 .AddProcessor(new WheelsCrawlerProcessor<Car> { })
                                 .AddPipeline(new WheelsCrawlerPipeline<Car> { });

            await crawler.Crawle();
        }
    }
}
