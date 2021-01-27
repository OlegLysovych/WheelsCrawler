using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WheelsCrawler.Core;
using WheelsCrawler.Data.Dto;
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
            // var crawlerRia = new WheelsCrawler<CarRiaDto, Car>()
            //                      .AddRequest(new WheelsCrawlerRequest { Url = "https://auto.ria.com/uk/legkovie/mercedes-benz/cl-class/?countpage=100", Regex = @".*/auto_[^ria].+", TimeOut = 5000 })
            //                      .AddDownloader(new WheelsCrawlerDownloader { DownloderType = WheelsCrawlerDownloaderType.FromFile, DownloadPath = "C:/Users/PC/source/repos/WheelsCrawler/htmls/RIA/"})
            //                      .AddProcessor(new WheelsCrawlerProcessor<CarRiaDto, Car> { })
            //                      .AddPipeline(new WheelsCrawlerPipeline<Car> { });

            // await crawlerRia.Crawle();

            var crawlerRST = new WheelsCrawler<CarRstDto, Car>()
                                 .AddRequest(new WheelsCrawlerRequest { Url = "https://rst.ua/ukr/oldcars/mercedes/cl-class/", Regex = @".*/oldcars/.+\.html$", TimeOut = 5000 })//[^.*/\d\2Ehtml$]
                                 .AddDownloader(new WheelsCrawlerDownloader { DownloderType = WheelsCrawlerDownloaderType.FromWeb, DownloadPath = @"C:/Users/PC/source/repos/WheelsCrawler/htmls/RST/"})//
                                 .AddProcessor(new WheelsCrawlerProcessor<CarRstDto, Car> { })
                                 .AddPipeline(new WheelsCrawlerPipeline<Car> { });

            await crawlerRST.Crawle();
        }
    }
}
